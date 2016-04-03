using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.Models;
using LojaMobile.ViewModels;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class CadastroClientePage : ContentPage
    {
        private static bool subscribed = false;

        public CadastroClientePage(Cliente cliente)
        {
            var viewModel = new CadastroClienteViewModel(cliente)
            {
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Telefone = cliente.Telefone,
                Id = cliente.Id
            };
            BindingContext = viewModel;
            Carregar();
        }

        public CadastroClientePage()
        {
            var viewModel = new CadastroClienteViewModel();
            BindingContext = viewModel;
            Carregar();
        }

        private void Carregar()
        {
            InitializeComponent();

            if (!subscribed)
            {
                MessagingCenter.Subscribe<string, string>("msgCadastroCliente", "alert",
                    (e, msg) => { DisplayAlert("Loja Mobile", msg, "Ok"); });

                subscribed = true;
            }
        }
    }
}
