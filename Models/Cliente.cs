using System.Collections.Generic;

namespace testelocadora.Models
{
    public class Cliente
    {
        public Cliente()
        {
        }

        public Cliente(int id, string nome, string cpf, bool ativo)
        {
            Id = id;
            Nome = nome;
            Ativo = ativo;
            Cpf = cpf;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
    }
}
