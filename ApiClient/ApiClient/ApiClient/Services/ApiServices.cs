using ApiClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Services
{
    public class ApiServices
    {
        private HttpClient client = new HttpClient();

        public async Task<Response> GetAll<T>(string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error de respuesta del servidor"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error de respuesta de los datos"
                };
            }
        }


        public async Task<Response> Delete<T>(string url)
        {
            try
            {

                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error de respuesta del servidor"
                    };
                }



                HttpResponseMessage response1 = await client.DeleteAsync(url);

                if (!response1.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se pudo eliminar"
                    };
                }

                return new Response
                {
                    IsSuccess = true,

                };

            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error de respuesta de los datos"
                };
            }
        }




        public async Task<Response> Post<T>(T model, string url)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error de respuesta del servidor"
                    };
                }
                return new Response
                {
                    IsSuccess = true,
                    Result = ""
                };
            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error al enviar los datos"
                };
            }
        }



        public async Task<Response> Put<T>(string url, T model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error de respuesta del servidor"
                    };
                }
                return new Response
                {
                    IsSuccess = true,
                    Result = ""
                };
            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error al enviar los datos"
                };
            }
        }

    }
}
