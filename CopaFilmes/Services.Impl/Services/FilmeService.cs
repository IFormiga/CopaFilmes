using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CopaFilmes.Domain.Entities.DTO;
using CopaFilmes.Domain.Repositories.Interfaces;
using CopaFilmes.Domain.Services.Interfaces;
using CopaFilmes.Entities.Entities;

namespace CopaFilmes.Services.Impl.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;
        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public IList<FilmeDTO> ObterPesquisaFilmes()
        {
            return _filmeRepository.ObterTodosFilmes()
                .Select(f => new FilmeDTO()
                {
                    Filme = new ChaveValorDTO<string>() { Chave = f.Id, Descricao = f.Titulo },
                    Lancamento = f.Ano
                }).ToList();
        }

        public IList<Filme> ObterFilmesPorIds(IList<string> idsFilmes)
        {
            return _filmeRepository.ObterFilmesPorIds(idsFilmes);
        }
    }
}
