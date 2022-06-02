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

        public async Task<Producto> GetProductoNameAsync(string name)
        {
            string request = "api/Productos/GetProductoName/" + name;
            Producto producto = await this.CallApiAsync<Producto>(request);
            return producto;
        }

        public async Task InsertProducto(Producto producto)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Productos/InsertProducto";

                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(producto);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Productos/UpdateProducto";
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(producto);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeleteProductoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Productos/" + id;
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task UpdateStockProductosAsync(int stock, string nombre)
        {
            string request = "api/Productos/UpdateStockProducto/" + stock + "/" + nombre;
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.PutAsync(request, null);
            }
        }

        public async Task<List<Producto>> GetProductosCategoriaAsync(string categoria)
        {
            string request = "api/Productos/GetProductosCategoria/" + categoria;
            List<Producto> productos = await this.CallApiAsync<List<Producto>>(request);
            return productos;
        }

        public async Task<List<Producto>> GetProductosBuscadorAsync(string palabra)
        {
            string request = "api/Productos/GetProductosBuscador/" + palabra;
            List<Producto> productos = await this.CallApiAsync<List<Producto>>(request);
            return productos;
        }
    }
}
