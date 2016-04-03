using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.ViewModels;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class EstoquePage : ContentPage
    {
        public EstoquePage()
        {
            BindingContext = new EstoqueViewModel(Navigation);

            InitializeComponent();
        }

        private void OnExcluir(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var id = (int)item.CommandParameter;

            var binding = (EstoqueViewModel) BindingContext;

            binding.OnExcluir(id);
        }
        private void OnEditar(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var id = (int)item.CommandParameter;

            var binding = (EstoqueViewModel)BindingContext;

            binding.OnEditar(id);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var binding = (EstoqueViewModel)BindingContext;
            binding.PopularLista();
        }
    }
}
