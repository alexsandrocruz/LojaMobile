using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.Core.Interfaces;
using LojaMobile.Models;
using SQLite.Net;
using Xamarin.Forms;

namespace LojaMobile.Core.DataLayer
{
    public class UsuarioDL
    {
        SQLiteConnection _connection;
        public UsuarioDL()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Usuario>();
        }

        public bool Salvar(Usuario usuario)
        {
            var r = _connection.Insert(usuario);

            return (r == 1) ? true : false;
        }

        public bool Logar(Usuario usuario)
        {
            // Lambda
            var user = _connection.Table<Usuario>()
                                  .FirstOrDefault(u => u.Nome == usuario.Nome && u.Senha == usuario.Senha);

            if (user != null)
            {
                DesativarUsuarios();

                user.Ativo = true;

                _connection.Update(user);
                return true;
            }

            return false;
        }

        public void DesativarUsuarios()
        {
            var usuarios = _connection.Table<Usuario>()
                                          .Where(u => u.Ativo == true)
                                          .ToList();

            foreach (var item in usuarios)
            {
                item.Ativo = false;
                _connection.Update(item);
            }
        }

        public bool UsuarioCadastrado(Usuario usuario)
        {
            var u = _connection.Table<Usuario>().Any(a => a.Nome == usuario.Nome);
            return u;
        }

        public Usuario UsuarioAtivo()
        {
            var usuario = _connection.Table<Usuario>().FirstOrDefault(u => u.Ativo == true);
            return usuario;
        }
    }
}
