using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LojaMobile.Core.DataLayer;
using LojaMobile.Models;
using LojaMobile.Views;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand Logar { get; set; }
        public ICommand Cadastrar { get; set; }

        private Usuario usuario;
        private INavigation navigation;

        public LoginViewModel(INavigation _navigation)
        {
            usuario = new Usuario();
            navigation = _navigation;

            Logar = new Command(OnLogar);
            Cadastrar = new Command(OnCadastrar);
        }

        private void OnCadastrar()
        {
            navigation.PushAsync(new RegistroPage());
        }

        private void OnLogar()
        {
            var r = new UsuarioDL().Logar(usuario);
            if (r == true)
            {
               App.Current.MainPage = new MenuPage(); 
            }
            else
            {
                MessagingCenter.Send("msgLogin", "alert","Usuário e senha não encontrados.");
            }
        }

        public string Nome
        {
            get { return usuario.Nome; }
            set { usuario.Nome = value; OnPropertyChanged(); }
        }

        public string Senha
        {
            get { return usuario.Senha; }
            set { usuario.Senha = value; OnPropertyChanged(); }
        }
    }
}
