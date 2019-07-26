using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Entities.Entities
{
    public class Campeonato
    {
        public IList<Filme> FilmesParticipantes { get; set; } = new List<Filme>();
        public IList<Partida> OitavasDeFinal { get; set; } = new List<Partida>();
        public IList<Partida> QuartasDeFinal { get; set; } = new List<Partida>();
        public Partida Final { get; set; }
    }
}
