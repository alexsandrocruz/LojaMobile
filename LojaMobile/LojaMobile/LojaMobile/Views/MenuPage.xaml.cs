using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        private MenuListPage menuLista;

        public MenuPage()
        {
            InitializeComponent();

            menuLista = new MenuListPage();

            menuLista.Menu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    var selected = (string)e.SelectedItem;
                    if (!string.IsNullOrEmpty(selected))
                    {
                        NavigateTo(selected);
                    }
                    else
                    {
                        ((ListView)sender).SelectedItem = null;
                    }
                }
            };

            Master = menuLista;
            Detail = new NavigationPage(new DashboardPage());
        }

        void NavigateTo(string tela)
        {
            if (menuLista == null)
                return;

            Page displayPage = null;

            switch (tela)
            {
                case "Painel":
                    displayPage = new DashboardPage();
                    break;
                case "Venda":
                    displayPage = new VendaPage();
                    break;
                case "Estoque":
                    displayPage = new EstoquePage();
                    break;
                case "Cliente":
                    displayPage = new ClientePage();
                    break;
                case "Suporte":
                    displayPage = new ChatPage();
                    break;
            }

            if (displayPage != null)
            {
                Detail = new NavigationPage(displayPage);
            }

            menuLista.Menu.SelectedItem = null;
            IsPresented = false;
        }
    }
}
