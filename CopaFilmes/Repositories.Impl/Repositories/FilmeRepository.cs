using CopaFilmes.Domain.Repositories.Impl.Contexts;
using CopaFilmes.Domain.Repositories.Interfaces;
using CopaFilmes.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopaFilmes.Domain.Repositories.Impl.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly ApplicationContext _context;
        public FilmeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IList<Filme> ObterTodosFilmes()
        {
            return _context.Filmes.ToList();
        }

        public IList<Filme> ObterFilmesPorIds(IList<string> idsFilmes)
        {
            return _context.Filmes.Where(f => idsFilmes.Contains(f.Id)).ToList();
        }
    }
}
