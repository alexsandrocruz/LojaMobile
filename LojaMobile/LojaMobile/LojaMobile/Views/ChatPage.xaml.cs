using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaMobile.ViewModels;
using Xamarin.Forms;

namespace LojaMobile.Views
{
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            BindingContext = new ChatViewModel();
            InitializeComponent();
        }
    }
}
