using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testelocadora.Models
{
    public class Filme
    {
        public Filme()
        {
        }

        public Filme(int id, string titulo, bool ativo)
        {
            Id = id;
            Titulo = titulo;
            Ativo = ativo;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool Ativo { get; set; }
    }
}
