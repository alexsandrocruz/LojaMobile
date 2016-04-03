using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.Core.Services;
using LojaMobile.Models;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public Dashboard dashboard { get; set; }
        public DashboardViewModel()
        {
            dashboard = new Dashboard();

            PopularDados();
        }

        public void PopularDados()
        {
            var api = new ApiCall();

            api.GetResponse<Dashboard>("dashboard").ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TotalVendas = t.Result.TotalVendas;
                        QuantidadeVendas = t.Result.QuantidadeVendas;
                    });
                }
            });
        }

        public double TotalVendas
        {
            get { return dashboard.TotalVendas; }
            set { dashboard.TotalVendas = value; OnPropertyChanged(); }
        }

        public int QuantidadeVendas
        {
            get { return dashboard.QuantidadeVendas; }
            set { dashboard.QuantidadeVendas = value; OnPropertyChanged(); }
        }
    }
}
