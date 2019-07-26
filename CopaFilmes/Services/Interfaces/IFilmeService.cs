using CopaFilmes.Domain.Entities.DTO;
using CopaFilmes.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Domain.Services.Interfaces
{
    public interface IFilmeService
    {
        /// <summary>
        /// Método que obtém todos os filmes cadastrados no banco
        /// </summary>
        /// <returns>Retorna uma lista de dtos com todos os filmes</returns>
        IList<FilmeDTO> ObterPesquisaFilmes();

        /// <summary>
        /// Método que obtém todos os filmes que tiverem um id na lista de idsFilmes 
        /// </summary>
        /// <param name="idsFilmes">Lista de ids usadas como filtro</param>
        /// <returns>Lista com todos os filmes filtrados.</returns>
        IList<Filme> ObterFilmesPorIds(IList<string> idsFilmes);
    }
}
