using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Domain.Entities.DTO
{
    public class ResultadoCampeonatoDTO
    {
        public ChaveValorDTO<string> FilmeCampeao { get; set; }
        public ChaveValorDTO<string> FilmeViceCampeao { get; set; }
    }
}
