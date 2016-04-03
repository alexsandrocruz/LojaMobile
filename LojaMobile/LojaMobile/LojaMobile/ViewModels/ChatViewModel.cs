using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LojaMobile.Core.DataLayer;
using LojaMobile.Models;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        IHubProxy _hub;
        string url = @"http://loja-chat.azurewebsites.net";
        HubConnection connection;
        private Usuario usuario;

        public ChatViewModel()
        {
            usuario = new UsuarioDL().UsuarioAtivo();
            Enviar = new Command(OnEnviar);
            ListaMensagens = new ObservableCollection<ChatMessage>();
            PopularLista();
        }

        private void PopularLista()
        {
            connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("ChatHub");

            _hub.On<string, string>("broadcastMessage", (name, message) => ListaMensagens.Add(new ChatMessage()
            {
                Date = DateTime.Now,
                Text = message,
                Name = name
            }));

            OnConnect();
        }

        private async void OnConnect()
        {
            await connection.Start();
        }

        private async void OnEnviar()
        {
            await _hub.Invoke("Send", usuario.Nome, Mensagem);
            Mensagem = string.Empty;
        }

        public ICommand Enviar { get; set; }

        private string _mensagem;
        public string Mensagem
        {
            get { return _mensagem; }
            set { _mensagem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ChatMessage> _listaMensagens;

        public ObservableCollection<ChatMessage> ListaMensagens
        {
            get { return _listaMensagens; }
            set
            {
                _listaMensagens = value;
                OnPropertyChanged();
            }
        }
    }
}
