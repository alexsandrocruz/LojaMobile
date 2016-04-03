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
    public class CadastroEstoqueViewModel : BaseViewModel
    {
        public ICommand Salvar { get; set; }
        public Estoque estoque;

        public CadastroEstoqueViewModel(Estoque _estoque)
        {
            estoque = _estoque;
            Carregar();
        }

        public CadastroEstoqueViewModel()
        {
            estoque = new Estoque();
            Carregar();
        }

        private void Carregar()
        {
            Salvar = new Command(OnSalvar);
            ListaGrupos = new ObservableCollection<string>();
            ListaGrupos.Add("Papelaria");
            ListaGrupos.Add("Confecção");
            ListaGrupos.Add("Informática");
            ListaGrupos.Add("Carro");
        }

        private void OnSalvar()
        {
            ApiCall api = new ApiCall();

            if (estoque.Id == 0)
            {
                api.Post<Estoque>("estoques", estoque).ContinueWith((t) =>
                {
                    if (t.IsCompleted)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            LimparViewModel();
                            MessagingCenter.Send("msgCadastroEstoque", "alert", "Estoque cadastrado.");
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send("msgCadastroEstoque", "alert", "Erro ao cadastrar estoque.");
                        });
                    }
                });
            }
            else
            {
                api.Update("estoques", estoque.Id, estoque).ContinueWith((t) =>
                {
                    if (t.IsCompleted)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            LimparViewModel();
                            MessagingCenter.Send("msgCadastroEstoque", "alert", "Estoque alterado.");
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send("msgCadastroEstoque", "alert", "Erro ao atualizar estoque.");
                        });
                    }
                });
            }

        }

        private void LimparViewModel()
        {
            Nome = string.Empty;
            Grupo = string.Empty;
            Valor = 0;
            Id = 0;
        }

        private ObservableCollection<string> _listaGrupos;

        public ObservableCollection<string> ListaGrupos
        {
            get { return _listaGrupos; }
            set { _listaGrupos = value; OnPropertyChanged(); }
        }

        public string Nome
        {
            get { return estoque.Nome; }
            set { estoque.Nome = value; OnPropertyChanged(); }
        }

        public string Grupo
        {
            get { return estoque.Grupo; }
            set { estoque.Grupo = value; OnPropertyChanged(); }
        }

        public double Valor
        {
            get { return estoque.Valor; }
            set { estoque.Valor = value; OnPropertyChanged(); }
        }

        public int Id
        {
            get { return estoque.Id; }
            set { estoque.Id = value; OnPropertyChanged(); }
        }
    }
}
