using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Entities.Entities
{
    public class Filme
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public decimal Nota { get; set; }
    }
}
