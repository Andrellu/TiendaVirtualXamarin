using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;
using TiendaVirtualXamarin.Views;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private ServiceUsuarios service;

        public LoginViewModel(ServiceUsuarios service)
        {
            this.service = service;
        }

        private Usuario _Usuario;

        public Usuario Usuario
        {
            get { return this._Usuario; }
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        private string _UserName;

        public string UserName
        {
            get { return this._UserName; }
            set
            {
                this._UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _Password;
        
        public string Password
        {
            get { return this._Password; }
            set
            {
                this._Password = value;
                OnPropertyChanged("Password");
            }
        }

        public Command RegistrarUsuario
        {
            get
            {
                return new Command(() =>
                {
                    MainMenuView view = App.ServiceLocator.MainMenuView;
                    view.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(RegistroUsuario)));
                    view.IsPresented = false;
                });
            }
        }

        public Command AccionIniciarSesion
        {
            get
            {
                return new Command(async () =>
                {
                    Usuario user = await this.LoginUsuarioAsync();
                    if (user != null)
                    {                        
                        MainMenuView view = App.ServiceLocator.MainMenuView;
                        view.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PerfilView)));
                        view.IsPresented = false;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("¡ADVERTENCIA!", "El usuario no existe o puso mal las credenciales.", "Ok");
                    }                    
                });
            }
        }

        private async Task<Usuario> LoginUsuarioAsync()
        {
            string token = await this.service.GetTokenAsync(this.UserName, this.Password);
            if (token != null)
            {
                Usuario user = await this.service.PerfilUsuarioAsync(token);

                // AQUI GUARDAMOS EL TOKEN.
                SessionService session = App.ServiceLocator.SessionService;
                session.Token = token;

                this.Usuario = user;
                App.ServiceLocator.SessionService.User = user;
                MessagingCenter.Send<MainMenuView>
                    (App.ServiceLocator.MainMenuView, "RELOADLOGIN");

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
