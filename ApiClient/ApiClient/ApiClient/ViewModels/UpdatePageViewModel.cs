using ApiClient.Models;
using ApiClient.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApiClient.ViewModels
{
    public class UpdatePageViewModel : BaseViewModel
    {

        #region Atributos
        private ApiServices api;
        private DialogServices _dialogService;
        private string _nombre;
        private string _apellido;
        private int _edad;

        //Creando el objeto que tendra el valor seleccionado o antiguo
        private Cliente old;

        #endregion
        #region Propiedades
        public string Nombre
        {
            get { return old.Nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return old.Apellido; }
            set { _apellido = value; }
        }
        public int Edad
        {
            get { return old.Edad; }
            set
            {
                _edad = value;
            }
        }

        public ICommand Update
        {
            get
            {
                return new RelayCommand(UpdatePerson);
            }
        }
        #endregion
        #region Constructor
        public UpdatePageViewModel(Cliente obj)
        {
            api = new ApiServices();
            this.old = obj;
            _dialogService = new DialogServices();
        }
        #endregion

        private async void UpdatePerson()
        {
            if (string.IsNullOrEmpty(_nombre))
            {
                await _dialogService.Message("Error", "El nombre es requerido");
                return;
            }
            if (string.IsNullOrEmpty(_apellido))
            {
                await _dialogService.Message("Error", "El apellido es requerido");
                return;
            }
            if (_edad <= 0)
            {
                await _dialogService.Message("Error", "La edad debe ser mayor a 0");
                return;
            }
            if (_edad >= 110)
            {
                await _dialogService.Message("Error", "La edad debe ser menor a 110");
                return;
            }

            var id = old.Id;

            Cliente client = new Cliente
            {
                Id = id,
                Nombre = this._nombre,
                Apellido = this._apellido,
                Edad = this._edad,
            };

            Uri baseUri = new Uri("http://10.0.2.2:64449/api/cliente/");

            var ingresado = await api.Put<Cliente>(baseUri.ToString(), client);
            if (!ingresado.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", ingresado.Message, "Ok");
                return;
            }
            await _dialogService.Message("Exitoso", "El registro ha sido actualizado");
            Application.Current.MainPage = new NavigationPage(new MainPage());

        }
    }
}
