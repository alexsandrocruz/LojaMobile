using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LojaMobile.Core.Services;
using LojaMobile.Models;
using LojaMobile.Views;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class ClienteViewModel : BaseViewModel
    {
        public ICommand Atualizar { get; set; }
        public ICommand Cadastrar { get; set; }
        public INavigation Navigation { get; set; }

        public ClienteViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Cadastrar = new Command(OnCadastrar);
            Atualizar = new Command(PopularLista);

            ListaCliente = new ObservableCollection<Cliente>();

            PopularLista();
        }

        public void OnExcluir(int id)
        {
            var api = new ApiCall();

            api.Delete("/clientes", id).ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(PopularLista);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send("msgEstoque", "alert", "Erro ao excluir.");
                    });
                }
            });
        }

        public void OnEditar(int id)
        {
            var item = ListaCliente.First(e => e.Id == id);

            var cadastroPage = new CadastroClientePage(item);

            Navigation.PushAsync(cadastroPage);
        }

        private void OnCadastrar()
        {
            Navigation.PushAsync(new CadastroClientePage());
        }

        public void PopularLista()
        {
            ApiCall api = new ApiCall();

            api.GetResponse<ObservableCollection<Cliente>>("clientes").ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    ListaCliente = t.Result;
                }
                else
                {
                    MessagingCenter.Send("msgEstoque", "alert", "Erro ao carregar cliente.");
                }
            });

            Carregando = false;
        }

        private bool _carregando;

        public bool Carregando
        {
            get { return _carregando; }
            set { _carregando = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Cliente> _listaCliente;

        public ObservableCollection<Cliente> ListaCliente
        {
            get { return _listaCliente; }
            set { _listaCliente = value; OnPropertyChanged(); }
        }
    }
}
