using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Entities.Entities
{
    public class Partida
    {
        public Filme CompetidorUm { get; set; }
        public Filme CompetidorDois { get; set; }
        public Filme Ganhador { get; set; }
    }
}
