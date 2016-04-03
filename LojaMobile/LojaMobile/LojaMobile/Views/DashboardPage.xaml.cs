using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.ViewModels;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            BindingContext = new DashboardViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var binding = (DashboardViewModel) BindingContext;
            binding.PopularDados();

            CrossLocalNotifications.Current.Show("Loja Mobile", "Notificação.");
        }
    }
}
