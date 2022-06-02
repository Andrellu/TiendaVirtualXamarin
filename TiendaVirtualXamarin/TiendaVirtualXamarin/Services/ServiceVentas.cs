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
    public class ServiceVentas
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceVentas(IConfiguration configuration)
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

        public async Task InsertVentaAsync(Venta venta, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Usuarios/InsertComprarUsuariosProducto";

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                string json = JsonConvert.SerializeObject(venta);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task<List<Venta>> GetAllVentas()
        {
            string request = "/api/Ventas";
            List<Venta> ventas = await this.CallApiAsync<List<Venta>>(request);
            return ventas;
        }

        public async Task<List<Venta>> GetVentasUsuario(string nameUser)
        {
            string request = "/api/Usuarios/GetProductosComprados/" + nameUser;
            List<Venta> ventas = await this.CallApiAsync<List<Venta>>(request);
            return ventas;
        }

        public async Task UpdateEstadoVentas(int idVenta)
        {
            string request = "api/Ventas/UpdateEstadoVenta/" + idVenta;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.PutAsync(request, null);
            }
        }

        public async Task DeleteVentasUsuarioAsync(string nombreUsuario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Ventas/" + nombreUsuario;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
