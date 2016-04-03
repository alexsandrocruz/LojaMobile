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
    public partial class CadastroEstoquePage : ContentPage
    {
        private static bool subscribed = false;

        public CadastroEstoquePage(Estoque estoque)
        {
            var viewModel = new CadastroEstoqueViewModel(estoque)
            {
                Nome = estoque.Nome,
                Grupo = estoque.Grupo,
                Valor = estoque.Valor,
                Id = estoque.Id
            };
            BindingContext = viewModel;
            Carregar();
        }

        public CadastroEstoquePage()
        {
            var viewModel = new CadastroEstoqueViewModel();
            BindingContext = viewModel;
            Carregar();
        }

        private void Carregar()
        {
            InitializeComponent();

            if (!subscribed)
            {
                MessagingCenter.Subscribe<string, string>("msgCadastroEstoque", "alert",
                    (e, msg) => { DisplayAlert("Loja Mobile", msg, "Ok"); });

                subscribed = true;
            }
        }
    }
}
