using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LojaMobile.Core.Services;
using LojaMobile.Models;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class VendaViewModel : BaseViewModel
    {
        public ICommand Vender { get; set; }
        private Venda venda;

        public VendaViewModel()
        {
            Vender = new Command(OnVender);

            venda = new Venda();

            ListaEstoque = new ObservableCollection<Estoque>();
            ListaCliente = new ObservableCollection<Cliente>();

            PopularLista();
        }

        private async void PopularLista()
        {
            var api = new ApiCall();

            await api.GetResponse<ObservableCollection<Estoque>>("estoques").ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ListaEstoque = t.Result;
                        Estoques = ListaEstoque.Select(e => $"{e.Nome} - R$ {e.Valor.ToString("N2")}").ToArray();
                    });
                }
            });

            await api.GetResponse<ObservableCollection<Cliente>>("clientes").ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ListaCliente = t.Result;
                        Clientes = ListaCliente.Select(e => e.Nome).ToArray();
                    });
                }
            });
        }

        private void OnVender(object obj)
        {
            var api = new ApiCall();

            venda.DataVenda = DateTime.Now;

            api.Post("vendas", venda).ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        LimparViewModel();
                        MessagingCenter.Send("msgVenda", "alert", "Venda efetuada");
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send("msgVenda", "alert", "Erro ao vender");
                    });
                }
            });
        }

        private void LimparViewModel()
        {
            NomeCliente = string.Empty;
            NomeEstoque = string.Empty;
            venda = new Venda();
            EstoqueIndex = 0;
            ClienteIndex = 0;
            MessagingCenter.Send("venda","limpar");
            PopularLista();
        }

        private string[] _estoques;
        public string[] Estoques
        {
            get { return _estoques; }
            set { _estoques = value; OnPropertyChanged(); }
        }

        private string[] _clientes;
        public string[] Clientes
        {
            get { return _clientes; }
            set { _clientes = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Estoque> _listaEstoque;
        public ObservableCollection<Estoque> ListaEstoque
        {
            get { return _listaEstoque; }
            set { _listaEstoque = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Cliente> _listaCliente;

        public ObservableCollection<Cliente> ListaCliente
        {
            get { return _listaCliente; }
            set { _listaCliente = value; OnPropertyChanged(); }
        }

        public Estoque Estoque
        {
            get { return venda.Estoque; }
            set { venda.Estoque = value; OnPropertyChanged(); }
        }

        public Cliente Cliente
        {
            get { return venda.Cliente; }
            set { venda.Cliente = value; OnPropertyChanged(); }
        }

        private string _nomeEstoque;
        public string NomeEstoque
        {
            get { return _nomeEstoque; }
            set { _nomeEstoque = value; OnPropertyChanged(); }
        }

        private string _nomeCliente;
        public string NomeCliente
        {
            get { return _nomeCliente; }
            set { _nomeCliente = value; OnPropertyChanged(); }
        }

        private int _estoqueIndex;
        public int EstoqueIndex
        {
            get { return _estoqueIndex; }
            set
            {
                _estoqueIndex = value;

                if (value != -1)
                {
                    Estoque = ListaEstoque.ElementAt(value);
                }
                
                OnPropertyChanged();
            }
        }

        private int _clienteIndex;
        public int ClienteIndex
        {
            get { return _clienteIndex; }
            set
            {
                _clienteIndex = value;

                if (value != -1)
                {
                    Cliente = ListaCliente.ElementAt(value);
                }
                
                OnPropertyChanged();
            }
        }
    }
}
