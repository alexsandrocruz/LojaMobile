using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LojaMobile.Core.DataLayer;
using LojaMobile.Models;
using LojaMobile.Views;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class RegistroViewModel : BaseViewModel
    {
        public Usuario usuario { get; set; }
        public ICommand Registrar { get; set; }
        public INavigation Navigation { get; set; }

        public RegistroViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            usuario = new Usuario();
            Registrar = new Command(OnRegistrar);

            MostrarSenha = false;

            DataMinima = DateTime.Today.AddYears(-130);
            DataMaxima = DateTime.Today;
            DataNascimento = DateTime.Today;

            ListaEstado = new ObservableCollection<string>();
            ListaEstado.Add("DF");
            ListaEstado.Add("RS");
            ListaEstado.Add("SP");
            ListaEstado.Add("RJ");
        }

        private void OnRegistrar()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                MessagingCenter.Send("msgRegistro", "alert", "Preencha o nome de usuário.");
                return;
            }

            if (string.IsNullOrEmpty(Senha))
            {
                MessagingCenter.Send("msgRegistro", "alert", "Preencha a senha.");
                return;
            }

            if (DataNascimento == DateTime.Today)
            {
                MessagingCenter.Send("msgRegistro", "alert", "Preencha a data de nascimento.");
                return;
            }

            if (string.IsNullOrEmpty(Celular))
            {
                MessagingCenter.Send("msgRegistro", "alert", "Preencha o celular.");
                return;
            }

            if (string.IsNullOrEmpty(Estado))
            {
                MessagingCenter.Send("msgRegistro", "alert", "Selecione o estado.");
                return;
            }

            var usuarioCadastrado = new UsuarioDL().UsuarioCadastrado(usuario);

            if (usuarioCadastrado)
            {
                MessagingCenter.Send("msgRegistro", "alert", "Usuário Cadastrado.");
                return;
            }

            var r = new UsuarioDL().Salvar(usuario);

            if (r == true)
            {
                MessagingCenter.Send("msgRegistro", "alert", "Cadastrado com sucesso!");
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MessagingCenter.Send("msgRegistro", "alert", "Erro ao cadastrar!");
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

        public DateTime DataNascimento
        {
            get { return usuario.DataNascimento; }
            set { usuario.DataNascimento = value; OnPropertyChanged(); }
        }

        private DateTime _dataMinima;
        public DateTime DataMinima
        {
            get { return _dataMinima; }
            set { _dataMinima = value; OnPropertyChanged(); }
        }

        private DateTime _dataMaxima;

        public DateTime DataMaxima
        {
            get { return _dataMaxima; }
            set { _dataMaxima = value; OnPropertyChanged(); }
        }

        public string Celular
        {
            get { return usuario.Celular; }
            set { usuario.Celular = value; OnPropertyChanged(); }
        }

        public string Estado
        {
            get { return usuario.Estado; }
            set { usuario.Estado = value; OnPropertyChanged(); }
        }

        private bool _mostrarSenha;
        public bool MostrarSenha
        {
            get { return _mostrarSenha; }
            set
            {
                _mostrarSenha = value;
                IsPassword = !value;
                OnPropertyChanged();
            }
        }

        private bool _isPassword;
        public bool IsPassword
        {
            get { return _isPassword; }
            set { _isPassword = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _listaEstado;
        public ObservableCollection<string> ListaEstado
        {
            get { return _listaEstado; }
            set { _listaEstado = value; OnPropertyChanged(); }
        }
    }
}
