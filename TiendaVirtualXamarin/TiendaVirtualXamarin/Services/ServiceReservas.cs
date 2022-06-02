using Microsoft.Extensions.Configuration;
using ModelsTVX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtualXamarin.Services
{
    public class ServiceReservas
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceReservas(IConfiguration configuration)
        {
            this.UrlApi = configuration["ApiUrl:ApiTiendaVirtual"];
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Reserva>> GetReservasUsuario(string nameUser)
        {
            string request = "api/Usuarios/GetReservasUsuario/" + nameUser;
            List<Reserva> reservas = await this.CallApiAsync<List<Reserva>>(request);
            return reservas;
        }

        public async Task<Reserva> GetReservaId(int id)
        {
            string request = "/api/Reservas/FindReserva/" + id;
            Reserva reserva = await this.CallApiAsync<Reserva>(request);
            return reserva;
        }

        public async Task CreateReserva(Reserva reserva)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Usuarios/InsertReservaUsuariosProducto";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(reserva);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task DeleteReservasUsuarioAsync(string nombreUsuario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Reservas/" + nombreUsuario;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task DeleteReservaUsuarioAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Reservas/DeleteReservaUsuario/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
