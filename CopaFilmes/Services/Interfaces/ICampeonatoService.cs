using CopaFilmes.Domain.Entities.DTO;
using CopaFilmes.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Domain.Services.Interfaces
{
    public interface ICampeonatoService
    {
        ResultadoCampeonatoDTO GerarCampeonato(IList<string> idsFilmesSelecionados);
    }
}
