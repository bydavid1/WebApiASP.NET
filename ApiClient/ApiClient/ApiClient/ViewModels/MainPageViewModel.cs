using ApiClient.Models;
using ApiClient.Services;
using ApiClient.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApiClient.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ApiServices api;
        private bool isRefreshing;
        private Cliente obj;
        private DialogServices _dialogService;
        public ObservableCollection<Cliente> Clientes { get; set; }

        public Cliente Obj
        {
            get { return obj; }
            set
            {
                obj = value; OnPropertyChange();
                if (value != null)
                {
                    Opcion();

                }
            }
        }

        public bool IsRefreshing
        {

            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChange(); }

        }

        public ICommand Refreshing
        {
            get
            {
                return new RelayCommand(Refresh);
            }
        }

        public ICommand NewClient

        {
            get
            {
                return new RelayCommand(NewPage);
            }
        }


        private void Refresh()
        {
            IsRefreshing = true;
            LoadClientes();
            IsRefreshing = false;
        }


        public MainPageViewModel()
        {
            Clientes = new ObservableCollection<Cliente>();
            api = new ApiServices();
            _dialogService = new DialogServices();
            LoadClientes();
            IsRefreshing = false;
        }

        private async void LoadClientes()
        {
            var response = await api.GetAll<Cliente>("http://10.0.2.2:64449/api/cliente");
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                return;
            }
            Clientes = (ObservableCollection<Cliente>)response.Result;
            IsRefreshing = false;

        }

        private async void Opcion()
        {
            bool opcion = await _dialogService.Message("Opciones", "Seleccione...", "Actualizar", "Eliminar");

            if (opcion == true)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new UpdatePage(Obj));
                Obj = null;
            }
            else
            {
                var respuesta = await _dialogService.Message("Confirmacion", "¿Desea eliminar este cliente?", "Aceptar", "Cancelar");
                if (respuesta == true)
                {
                    var id = obj.Id;
                    Uri baseUri = new Uri("http://10.0.2.2:64449/api/cliente/" + id);
                    var response = await api.Delete<Cliente>(baseUri.ToString());

                    if (!response.IsSuccess)
                    {
                        IsRefreshing = false;
                        await App.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                        return;
                    }
                    await _dialogService.Message("Eliminado", "Registro eliminado");
                    LoadClientes();
                }
                else
                {
                    Obj = null;
                }
            }
        }

        private void NewPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new NewPage());
        }
    }
}

