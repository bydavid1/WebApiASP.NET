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
   public class NewPageViewModel : BaseViewModel
    {
        #region Atributos
        private ApiServices api;
        private DialogServices _dialogService;
        private string _nombre;
        private string _apellido;
        private int _edad;
        #endregion

        #region Propiedades
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; OnPropertyChange(); }
        }
        public string apellido
        {
            get { return _apellido; }
            set { _apellido = value; OnPropertyChange(); }
        }

        public int edad
        {
            get { return _edad; }
            set { _edad = value; OnPropertyChange(); }
        }
        #endregion

        #region Constructor
        public NewPageViewModel()
        {
            api = new ApiServices();
            _dialogService = new DialogServices();
        }
        #endregion

        public ICommand Add
        {
            get
            {
                return new RelayCommand(AddNewCliente);
            }
        }

        private async void AddNewCliente()
        {
            if (string.IsNullOrEmpty(nombre))
            {
                await _dialogService.Message("Error", "El nombre es requerido");
                return;
            }
            if (string.IsNullOrEmpty(apellido))
            {
                await _dialogService.Message("Error", "El apellido es requerido");
                return;
            }
            if (edad <= 0)
            {
                await _dialogService.Message("Error", "La edad debe ser mayor a 0");
                return;
            }
            if (edad >= 110)
            {
                await _dialogService.Message("Error", "La edad debe ser menor a 110");
                return;
            }

            Cliente client = new Cliente
            {
                Nombre = this.nombre,
                Apellido = this.apellido,
                Edad = this.edad
            };

            var ingresado = await api.Post<Cliente>(client, "http://10.0.2.2:60424/api/clientes");

            if (!ingresado.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", ingresado.Message, "Ok");
                return;
            }

            await _dialogService.Message("Exitoso", "El registro ha sido guardado");
            Application.Current.MainPage = new NavigationPage(new MainPage());

        }
    }
}
