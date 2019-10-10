using ApiClient.Models;
using ApiClient.ViewModels;
using ApiClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApiClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            if (item != null)
            {
             var f = item.BindingContext as Cliente;
                Navigation.PushAsync(new UpdatePage(f));
            }
        }
    }
}
