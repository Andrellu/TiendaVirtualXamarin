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
                return new Command(async () =>
                {
                    
                    RegistroUsuario view = new RegistroUsuario();
                    RegistroUsuarioViewModel viewmodel = App.ServiceLocator.RegistroUsuarioViewModel;

                    view.BindingContext = viewmodel;

                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
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
                        /*
                       Aqui generamos la view de perfil.

                       Centros view = new Centros();

                       CentrosListViewModel viewmodel = App.ServiceLocator.CentrosListViewModel;

                       view.BindingContext = viewmodel;

                       //Redirecciona a la vista centros
                       await Application.Current.MainPage.Navigation.PushAsync(view);
                       */
                    }
                    await App.Current.MainPage.DisplayAlert("¡ADVERTENCIA!", "El usuario no existe o puso mal las credenciales.", "Ok");
                });
            }
        }

        private async Task<Usuario> LoginUsuarioAsync()
        {
            string token = await this.service.GetTokenAsync(this.UserName, this.Password);
            if (token != null)
            {
                Usuario user = await this.service.PerfilUsuarioAsync(token);              
                    SessionService session =App.ServiceLocator.SessionService;
                    session.Token = token;
                App.ServiceLocator.SessionService.User = user;
                this.Usuario = user;

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
