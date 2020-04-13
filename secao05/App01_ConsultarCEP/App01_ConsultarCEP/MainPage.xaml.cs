using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;


namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            

            BOTAO.Clicked += BuscarCep;
        }

        private void BuscarCep(object sender, EventArgs args) {

            string cep = CEP.Text.Trim();

            if(isValidCep(cep)) {

                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCep(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0},{1},{2},{3}", end.localidade, end.uf, end.logradouro, end.bairro);

                    }
                    else
                    {
                        DisplayAlert("Erro!", "Endereço não encontrado para o CEP digitado! : " +cep , "OK");
                    }
                }
                catch (Exception e){
                    DisplayAlert("Erro!","Falha! Tente novamente mais tarde!: "+e.Message,"OK");
                }   
            }
            

        }

        private bool isValidCep(string cep)
        {
            bool valido = true;

            if (cep.Length != 8) {

                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 números.", "OK");    
                valido = false;

            }

            int NovoCep = 0;

            if (!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter apenas 8 números.", "OK");
                valido = false;
            }
            return valido;
        }
    }
}
