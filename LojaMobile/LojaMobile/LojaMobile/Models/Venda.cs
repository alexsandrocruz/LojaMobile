using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaMobile.Models
{
    public class Venda
    {
        public Venda()
        {
            Cliente = new Cliente();
            Estoque = new Estoque();
        }

        public DateTime DataVenda { get; set; }
        public Cliente Cliente { get; set; }
        public Estoque Estoque { get; set; }
    }
}

