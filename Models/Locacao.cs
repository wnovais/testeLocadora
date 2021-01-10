using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testelocadora.Models
{
    public class Locacao
    {
        public Locacao()
        {
        }

        public Locacao(int id, int idCliente, int idFilme, bool statusLocacao, DateTime dataLocacao, DateTime? dataDevolucao)
        {
            Id = id;
            IdCliente = idCliente;
            IdFilme = idFilme;
            StatusLocacao = statusLocacao;
            DataLocacao = dataLocacao;
            DataDevolucao = dataDevolucao;
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFilme { get; set; }
        public bool StatusLocacao{ get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime? DataDevolucao { get; set; }
    }
}
