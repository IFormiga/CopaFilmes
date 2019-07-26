using CopaFilmes.Domain.Entities.DTO;
using CopaFilmes.Domain.Services.Interfaces;
using CopaFilmes.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopaFilmes.Domain.Services.Impl.Services
{
    public class CampeonatoService : ICampeonatoService
    {
        private readonly IFilmeService _filmeService;
        public CampeonatoService(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        public ResultadoCampeonatoDTO GerarCampeonato(IList<string> idsFilmesSelecionados)
        {
            ValidarNumeroDeFilmesSelecionados(idsFilmesSelecionados);

            var campeonato = new Campeonato()
            {
                FilmesParticipantes = _filmeService.ObterFilmesPorIds(idsFilmesSelecionados)
            };

            campeonato.FilmesParticipantes = campeonato.FilmesParticipantes.OrderBy(f => f.Titulo).ToList();
            RealizarOitavasDeFinal(campeonato);
            RealizarQuartasDeFinal(campeonato);
            RealizarFinal(campeonato);
            var filmeViceCampeao = campeonato.Final.Ganhador.Equals(campeonato.Final.CompetidorUm) ? campeonato.Final.CompetidorDois : campeonato.Final.CompetidorUm;
            return new ResultadoCampeonatoDTO()
            {
                FilmeCampeao = new ChaveValorDTO<string>() { Chave = campeonato.Final.Ganhador.Id, Descricao = campeonato.Final.Ganhador.Titulo },
                FilmeViceCampeao = new ChaveValorDTO<string>() { Chave = filmeViceCampeao.Id, Descricao = filmeViceCampeao.Titulo}
            };
        }

        /// <summary>
        /// Realiza a partida final
        /// </summary>
        /// <param name="campeonato">Recebe o campeonato que participa</param>
        private void RealizarFinal(Campeonato campeonato)
        {
            var finalistas = campeonato.QuartasDeFinal.Select(p => p.Ganhador).ToList();

            campeonato.Final = new Partida() { CompetidorUm = finalistas[0], CompetidorDois = finalistas[1] };
            RealizarPartida(campeonato.Final);
        }

        /// <summary>
        /// Método que realiza as quartas de final
        /// </summary>
        /// <param name="campeonato">Recebe o campeonato que participa</param>
        private void RealizarQuartasDeFinal(Campeonato campeonato)
        {
            var listaGanhadores = campeonato.OitavasDeFinal.Select(p => p.Ganhador).ToList();

            for (int i = 0; i < campeonato.OitavasDeFinal.Count; i += 2)
            {
                var novaPartida = new Partida()
                {
                    CompetidorUm = campeonato.OitavasDeFinal[i].Ganhador,
                    CompetidorDois = campeonato.OitavasDeFinal[i + 1].Ganhador
                };
                RealizarPartida(novaPartida);
                campeonato.QuartasDeFinal.Add(novaPartida);
            }
        }

        /// <summary>
        /// Método que realiza as partidas das oitavas de final
        /// </summary>
        /// <param name="campeonato">Recebe o campeonato que participa</param>
        private void RealizarOitavasDeFinal(Campeonato campeonato)
        {
            var ultimoIndex = campeonato.FilmesParticipantes.Count - 1;

            for (int i = 0; i < (campeonato.FilmesParticipantes.Count/2); i++)
            {
                var novaPartida = new Partida()
                {
                    CompetidorUm = campeonato.FilmesParticipantes[i],
                    CompetidorDois = campeonato.FilmesParticipantes[ultimoIndex - i]
                };
                RealizarPartida(novaPartida);
                campeonato.OitavasDeFinal.Add(novaPartida);
            }
        }

        /// <summary>
        /// Método que realiza uma partida.
        /// </summary>
        /// <param name="partida">Partida que será realizada</param>
        private void RealizarPartida(Partida partida)
        {
            ValidarRealizacaoPartida(partida);

            bool filmesComNotaIgual = partida.CompetidorUm.Nota == partida.CompetidorDois.Nota ? true : false;

            if (!filmesComNotaIgual)
            {
                partida.Ganhador = partida.CompetidorUm.Nota > partida.CompetidorDois.Nota ? partida.CompetidorUm : partida.CompetidorDois;
            }
            else
            {
                IList<Filme> filmes = new List<Filme>();
                filmes.Add(partida.CompetidorUm);
                filmes.Add(partida.CompetidorDois);

                partida.Ganhador = filmes.OrderBy(f => f.Titulo).ToList().First();
            }
        }

        #region Validações
        /// <summary>
        /// Valida o número de filmes selecionados para verificar se está dentro do aceitado
        /// </summary>
        /// <param name="idsFilmesSelecionados">Lista dos ids de filmes selecionados</param>
        private void ValidarNumeroDeFilmesSelecionados(IList<string> idsFilmesSelecionados)
        {
            if (idsFilmesSelecionados.Count != 8)
            {
                throw new ArgumentOutOfRangeException("idsFilmesSelecionados","O número de filmes selecionados precisa ser igual a 8");
            }
        }

        /// <summary>
        /// Valida tudo necessário para a realização de uma partida.
        /// </summary>
        /// <param name="partida">Partida será validada</param>
        private void ValidarRealizacaoPartida(Partida partida)
        {
            ValidarNulidadePartida(partida);
            ValidarNulidadeCompetidores(partida);
        }

        /// <summary>
        /// Método que valida se os competidores de uma partida estão ou não nulos.
        /// </summary>
        /// <param name="partida">Partida que contém os competidores a serem validados</param>
        private static void ValidarNulidadeCompetidores(Partida partida)
        {
            if (partida.CompetidorUm == null || partida.CompetidorDois == null)
            {
                throw new ArgumentNullException("Nenhum dos competidores de uma partida podem ser nulos.");
            }
        }

        /// <summary>
        /// Método que valida se a partida que está sendo utilizada é ou não nula.
        /// </summary>
        /// <param name="partida">A partida a ser validada.</param>
        private void ValidarNulidadePartida(Partida partida)
        {
            if (partida == null)
            {
                throw new ArgumentNullException("Partida não pode ser nulo.");
            }
        }
        #endregion
    }
}
