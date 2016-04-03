using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace LojaMobile.Models
{
    public class Usuario
    {
        public Usuario()
        {
                
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Celular { get; set; }
        public string Estado { get; set; }
        public bool Ativo { get; set; }

    }
}
