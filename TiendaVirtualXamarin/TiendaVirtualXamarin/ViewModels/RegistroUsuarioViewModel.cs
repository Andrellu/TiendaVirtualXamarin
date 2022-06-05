using System;
using System.Collections.Generic;
using System.Text;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;
using TiendaVirtualXamarin.Views;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class RegistroUsuarioViewModel : ViewModelBase
    {
        private ServiceUsuarios service;

        public RegistroUsuarioViewModel(ServiceUsuarios service)
        {
            this.service = service;
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

        private string _Nombre;
        public string Nombre
        {
            get { return this._Nombre; }
            set
            {
                this._Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        private string _ApellidoUno;
        public string ApellidoUno
        {
            get { return this._ApellidoUno; }
            set
            {
                this._ApellidoUno = value;
                OnPropertyChanged("ApellidoUno");
            }
        }

        private string _ApellidoDos;
        public string ApellidoDos
        {
            get { return this._ApellidoDos; }
            set
            {
                this._ApellidoDos = value;
                OnPropertyChanged("ApellidoDos");
            }
        }

        private string _Direccion;
        public string Direccion
        {
            get { return this._Direccion; }
            set
            {
                this._Direccion = value;
                OnPropertyChanged("Direccion");
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
                    await this.service.CreateUser(this.UserName, this.Nombre, this.ApellidoUno, this.ApellidoDos,
                                                  this.Direccion, this.Password);
                    await Application.Current.MainPage.DisplayAlert("ADVERTENCIA:", "Usuario registrado con éxito.", "Ok");
                    
                    LoginView view = new LoginView();
                    LoginViewModel viewmodel = App.ServiceLocator.LoginViewModel;

                    view.BindingContext = viewmodel;

                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}