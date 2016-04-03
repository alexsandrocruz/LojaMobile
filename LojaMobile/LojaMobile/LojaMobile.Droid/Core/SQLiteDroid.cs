using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LojaMobile.Core.Interfaces;
using LojaMobile.Droid.Core;
using SQLite.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDroid))]

namespace LojaMobile.Droid.Core
{
    public class SQLiteDroid : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var arquivo = "LojaMobile.db3";
            var caminhoDocumento = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var caminho = System.IO.Path.Combine(caminhoDocumento, arquivo);

            var plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conexao = new SQLiteConnection(plataforma, caminho);

            return conexao;
        }
    }
}