using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Services
{
    public class DialogServices
    {
        public async Task Message(string titulo, string sms)
        {
            await App.Current.MainPage.DisplayAlert(titulo, sms, "Aceptar");
        }
        public async Task<bool> Message(string titulo, string sms, string ok, string no)
        {
            var rs = await App.Current.MainPage.DisplayAlert(titulo, sms, ok, no);
            if (!rs)
            {
                return false;
            }
            return true;
        }
    }
}
