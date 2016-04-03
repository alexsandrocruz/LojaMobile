using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LojaMobile.Core.Services;
using LojaMobile.Models;
using Xamarin.Forms;

namespace LojaMobile.ViewModels
{
    public class CadastroClienteViewModel : BaseViewModel
    {
        public ICommand Salvar { get; set; }
        public Cliente cliente;

        public CadastroClienteViewModel(Cliente _cliente)
        {
            cliente = _cliente;
            Carregar();
        }

        public CadastroClienteViewModel()
        {
            cliente = new Cliente();
            Carregar();
        }

        private void Carregar()
        {
            Salvar = new Command(OnSalvar);
        }

        private void OnSalvar()
        {
            ApiCall api = new ApiCall();

            if (cliente.Id == 0)
            {
                api.Post<Cliente>("clientes", cliente).ContinueWith((t) =>
                {
                    if (t.IsCompleted)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            LimparViewModel();
                            MessagingCenter.Send("msgCadastroCliente", "alert", "Cliente cadastrado.");
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send("msgCadastroCliente", "alert", "Erro ao cadastrar cliente.");
                        });
                    }
                });
            }
            else
            {
                api.Update("clientes", cliente.Id, cliente).ContinueWith((t) =>
                {
                    if (t.IsCompleted)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            LimparViewModel();
                            MessagingCenter.Send("msgCadastroCliente", "alert", "Cliente alterado.");
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send("msgCadastroCliente", "alert", "Erro ao atualizar cliente.");
                        });
                    }
                });
            }

        }

        private void LimparViewModel()
        {
            Nome = string.Empty;
            CPF = string.Empty;
            Telefone = string.Empty;
            Id = 0;
        }

        public string Nome
        {
            get { return cliente.Nome; }
            set { cliente.Nome = value; OnPropertyChanged(); }
        }

        public string CPF
        {
            get { return cliente.CPF; }
            set { cliente.CPF = value; OnPropertyChanged(); }
        }

        public string Telefone
        {
            get { return cliente.Telefone; }
            set { cliente.Telefone = value; OnPropertyChanged(); }
        }

        public int Id
        {
            get { return cliente.Id; }
            set { cliente.Id = value; OnPropertyChanged(); }
        }
    }
}
