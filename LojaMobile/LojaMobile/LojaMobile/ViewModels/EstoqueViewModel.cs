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
    public class EstoqueViewModel : BaseViewModel
    {
        public ICommand Atualizar { get; set; }
        public ICommand Cadastrar { get; set; }
        public INavigation Navigation { get; set; }

        public EstoqueViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Cadastrar = new Command(OnCadastrar);
            Atualizar = new Command(PopularLista);

            ListaEstoque = new ObservableCollection<Estoque>();

            PopularLista();
        }

        public void OnExcluir(int id)
        {
            var api = new ApiCall();

            api.Delete("/estoques", id).ContinueWith((t) =>
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
            var item = ListaEstoque.First(e => e.Id == id);

            var cadastroPage = new CadastroEstoquePage(item);

            Navigation.PushAsync(cadastroPage);
        }

        private void OnCadastrar()
        {
            Navigation.PushAsync(new CadastroEstoquePage());
        }

        public void PopularLista()
        {
            ApiCall api = new ApiCall();

            api.GetResponse<ObservableCollection<Estoque>>("estoques").ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    ListaEstoque = t.Result;
                }
                else
                {
                    MessagingCenter.Send("msgEstoque", "alert", "Erro ao carregar estoque.");
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

        private ObservableCollection<Estoque> _listaEstoque;

        public ObservableCollection<Estoque> ListaEstoque
        {
            get { return _listaEstoque; }
            set { _listaEstoque = value; OnPropertyChanged(); }
        }
    }
}
