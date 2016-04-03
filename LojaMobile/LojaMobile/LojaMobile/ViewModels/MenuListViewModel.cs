using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LojaMobile.Core.DataLayer;
using LojaMobile.Views;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class MenuListViewModel : BaseViewModel
    {
        public ICommand Sair{ get; set; }
        public MenuListViewModel()
        {
            Sair = new Command(OnSair);

            ListaMenu = new ObservableCollection<string>();
            ListaMenu.Add("Painel");
            ListaMenu.Add("Venda");
            ListaMenu.Add("Estoque");
            ListaMenu.Add("Cliente");
            ListaMenu.Add("Suporte");
        }

        private void OnSair()
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
            new UsuarioDL().DesativarUsuarios();
            CrossLocalNotifications.Current.Show("Loja Mobile", "Você acabou de deslogar!");
        }

        private ObservableCollection<string> _listaMenu;
        public ObservableCollection<string> ListaMenu
        {
            get { return _listaMenu; }
            set { _listaMenu = value; OnPropertyChanged(); }
        }
    }
}
