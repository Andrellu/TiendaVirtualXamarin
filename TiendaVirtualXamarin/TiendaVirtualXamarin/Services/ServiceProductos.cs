using Microsoft.Extensions.Configuration;
using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtualXamarin.Services
{
    public class ServiceProductos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceProductos(IConfiguration configuration)
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

        public async Task<List<Producto>> GetProductosAsync()
        {
            string request = "api/Productos";
            List<Producto> productos = await this.CallApiAsync<List<Producto>>(request);
            return productos;
        }

        public async Task<Producto> FindProducto(int id)
        {
            string request = "api/Productos/" + id;
            Producto producto = await this.CallApiAsync<Producto>(request);
            return producto;
        }
    }
}
