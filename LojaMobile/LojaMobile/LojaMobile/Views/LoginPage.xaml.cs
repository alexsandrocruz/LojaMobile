using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.ViewModels;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class LoginPage : ContentPage
    {
        private static bool _subscribed = false;

        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new LoginViewModel(Navigation);

            InitializeComponent();

            if (!_subscribed)
            {
                MessagingCenter.Subscribe<string, string>("msgLogin", "alert", (e, msg) =>
                {
                    DisplayAlert("Loja Mobile", msg, "Ok");
                });

                _subscribed = true;
            }
        }
    }
}
