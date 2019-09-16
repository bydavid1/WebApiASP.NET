using ApiClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePage : ContentPage
    {
        public UpdatePage(Models.Cliente obj)
        {
            InitializeComponent();
            BindingContext = new UpdatePageViewModel(obj);
        }
    }
}