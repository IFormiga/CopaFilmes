using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Domain.Entities.DTO
{
    public class FilmeDTO
    {
        public ChaveValorDTO<string> Filme { get; set; }
        public int Lancamento { get; set; }
    }
}
