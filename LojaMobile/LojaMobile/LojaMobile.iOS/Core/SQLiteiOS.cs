using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LojaMobile.Core.Interfaces;
using LojaMobile.iOS.Core;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteiOS))]

namespace LojaMobile.iOS.Core
{
    public class SQLiteiOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var arquivo = "LojaMobile.db3";

            var caminhoDocumento = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var caminhoBiblioteca = Path.Combine(caminhoDocumento, "..", "Library");

            var caminho = Path.Combine(caminhoBiblioteca, arquivo);

            var plataforma = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var conexao = new SQLite.Net.SQLiteConnection(plataforma, caminho);
            return conexao;
        }
    }
}
