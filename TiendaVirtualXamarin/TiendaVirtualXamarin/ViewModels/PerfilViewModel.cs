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
    public class PerfilViewModel: ViewModelBase
    {
        private ServiceUsuarios service;

        public PerfilViewModel(ServiceUsuarios service)
        {
            Task.Run(() =>
            {
                Usuario user =  this.LoadUsuario();
            });
        }

        private Usuario LoadUsuario()
        {
            string token = App.ServiceLocator.SessionService.Token;

            Usuario user = App.ServiceLocator.SessionService.User;
            return user;
        }

        public Command MostarCarrito
        {
            get
            {
                return new Command(async () =>
                {
                    CarritoView view = new CarritoView();
                    CarritoViewModel viewmodel = App.ServiceLocator.CarritoViewModel;
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
