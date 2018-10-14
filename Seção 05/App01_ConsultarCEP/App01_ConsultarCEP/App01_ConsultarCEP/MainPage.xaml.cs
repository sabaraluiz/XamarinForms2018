using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            butBotao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = entCEP.Text.Trim();
            if (validaCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        //lblResultado.Text = "Endereço: " + end.logradouro + " " + end.complemento + " " + end.bairro + " " + end.localidade + " " + end.uf;
                        lblResultado.Text = string.Format("Endereço: {0} {1} {2} {3} {4}", end.logradouro, end.complemento, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("Erro", "Endereço não encontrado para o CEP.", "Ok");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro crítico", e.Message, "Ok");
                }
            }

        }
        private bool validaCEP(string cep)
        {
            bool valido = true;
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "O CEP deve conter 8 digitos.", "Ok");
                valido = false;
            }
            int num = 0;
            if (!int.TryParse(cep, out num))
            {
                DisplayAlert("Erro", "O CEP deve conter apenas números.", "Ok");
                valido = false;
            }
            return valido;
        }
    }
}
