using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Domain.Entities.DTO
{
    public class ChaveValorDTO<T> : IEquatable<ChaveValorDTO<T>>
    {
        public ChaveValorDTO() { }

        public ChaveValorDTO(T chave, string descricao)
        {
            this.Chave = chave;
            this.Descricao = descricao;
        }

        public T Chave { get; set; }
        public string Descricao { get; set; }

        public bool Equals(ChaveValorDTO<T> other)
        {
            return Chave.Equals(other.Chave) && Descricao.Equals(other.Descricao);
        }
    }
}
