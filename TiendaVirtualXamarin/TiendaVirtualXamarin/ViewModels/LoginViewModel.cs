using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;
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
                    //Navegamos al login
                    /*
                      Aqui generamos la view de perfil.

                      Centros view = new Centros();

                      CentrosListViewModel viewmodel = App.ServiceLocator.CentrosListViewModel;

                      view.BindingContext = viewmodel;

                      //Redirecciona a la vista centros
                      await Application.Current.MainPage.Navigation.PushAsync(view);
                  */
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
                /*
                    SessionService session =App.ServiceLocator.SessionService;
                    session.Token = token;
                */
                this.Usuario = user;
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
