using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.ViewModels;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class RegistroPage : ContentPage
    {
        private static bool subscribed = false;
        public RegistroPage()
        {
            BindingContext = new RegistroViewModel(Navigation);
            InitializeComponent();

            if (!subscribed)
            {
                MessagingCenter.Subscribe<string, string>("msgRegistro", "alert", (e, msg) =>
                {
                    DisplayAlert("Loja Mobile", msg, "Ok");
                });

                subscribed = true;
            }
        }
    }
}
