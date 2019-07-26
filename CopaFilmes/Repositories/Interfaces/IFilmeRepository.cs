using CopaFilmes.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Domain.Repositories.Interfaces
{
    public interface IFilmeRepository
    {
        IList<Filme> ObterTodosFilmes();
        IList<Filme> ObterFilmesPorIds(IList<string> idsFilmes);
    }
}
