using Microsoft.Extensions.Configuration;
using ModelsTVX;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtualXamarin.Services
{
    public class ServiceUsuarios
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceUsuarios(IConfiguration configuration)
        {
            this.UrlApi = configuration["ApiUrl:ApiTiendaVirtual"];
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel model = new LoginModel
                {
                    Username = username,
                    Passwordo = password
                };
                string json = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                string request = "api/login/ValidarUsuario";
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(data);
                    string token = jObject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
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

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            string request = "api/Usuarios/GetAllUsuarios";
            List<Usuario> users = await this.CallApiAsync<List<Usuario>>(request);
            return users;
        }

        public async Task<Usuario> PerfilUsuarioAsync(string token)
        {
            string request = "api/Usuarios/GetPerfilUsuario";
            Usuario user = await this.CallApiAsync<Usuario>(request, token);
            return user;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(string token, int id)
        {
            string request = "/api/Usuarios/FindUsuario/" + id;
            Usuario user = await this.CallApiAsync<Usuario>(request, token);
            return user;

        }

        public async Task<Usuario> GetUsuarioAsync(int idUsuario, string token)
        {
            string request = "api/Usuarios/FindUsuario/" + idUsuario;
            Usuario user = await this.CallApiAsync<Usuario>(request, token);
            return user;
        }

        public async Task DeleteUsuarioAsync(int idUser)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Usuarios/" + idUser;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task<List<Venta>> GetVentasUsuario(string nombreUser)
        {
            string request = "api/Usuarios/GetProductosComprados/" + nombreUser;
            List<Venta> compras = await this.CallApiAsync<List<Venta>>(request);
            return compras;
        }

        public async Task<List<Reserva>> GetReservasUsuario(string nombreUser)
        {
            string request = "/api/Usuarios/GetReservasUsuario/" + nombreUser;
            List<Reserva> reservas = await this.CallApiAsync<List<Reserva>>(request);
            return reservas;
        }

        public async Task CreateUser(string usuario, string name, string apellidoUno, string apellidoDos, string direccion, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Usuarios/CreateUsuario";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                RegisterUsuario user = new RegisterUsuario()
                {
                    NombreUsuario = usuario,
                    Nombre = name,
                    Apellidos = apellidoUno + " " + apellidoDos,
                    Direccion = direccion,
                    Password = password
                };
                string json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdateUsuario(Usuario user)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Usuarios/UpdateUsuario";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task UpdateMonedero(int saldo, int idUser)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Usuarios/UpdateMonedero/" + saldo + "/" + idUser;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.PutAsync(request, null);
            }
        }

        public async Task UpdateTarjetaCredito(int idUser, string numTarjeta, string fecha, int cvc)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Usuarios/UpdateCreditCARD/" + idUser + "/" + numTarjeta + "/" + fecha + "/" + cvc;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.PutAsync(request, null);
            }
        }
    }
}
