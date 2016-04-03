using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.ViewModels;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class VendaPage : ContentPage
    {
        private static bool subscribed = false;
        public VendaPage()
        {
            BindingContext = new VendaViewModel();
            InitializeComponent();

            if (!subscribed)
            {
                MessagingCenter.Subscribe<string, string>("msgVenda", "alert", (e, msg) =>
                {
                    DisplayAlert("Loja Mobile", msg, "Ok");
                });

                MessagingCenter.Subscribe<string>("venda", "limpar", (e) =>
                {
                    pckCliente.Items.Clear();
                    pckEstoque.Items.Clear();
                });

                subscribed = true;

            }
        }
    }
}
