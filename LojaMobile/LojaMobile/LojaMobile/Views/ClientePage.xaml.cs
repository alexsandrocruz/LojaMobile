using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.ViewModels;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class ClientePage : ContentPage
    {
        public ClientePage()
        {
            BindingContext = new ClienteViewModel(Navigation);

            InitializeComponent();
        }

        private void OnExcluir(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var id = (int)item.CommandParameter;

            var binding = (ClienteViewModel)BindingContext;

            binding.OnExcluir(id);
        }
        private void OnEditar(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var id = (int)item.CommandParameter;

            var binding = (ClienteViewModel)BindingContext;

            binding.OnEditar(id);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var binding = (ClienteViewModel)BindingContext;
            binding.PopularLista();
        }
    }
}
