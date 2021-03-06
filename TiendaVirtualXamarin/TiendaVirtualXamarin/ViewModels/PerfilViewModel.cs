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

        private Usuario _User;

        public Usuario Usuario
        {
            get { return this._User; }
            set
            {
                this._User = value;
                OnPropertyChanged("Usuario");
            }
        }

        public PerfilViewModel()
        {
            this.Usuario = App.ServiceLocator.SessionService.User;
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

        public Command MostrarVentas
        {
            get
            {
                return new Command(async () =>
                {
                    VentasUsuarioView view = new VentasUsuarioView();
                    VentasUsuarioViewModel viewmodel = App.ServiceLocator.VentasUsuarioViewModel;
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }

        public Command LogOut
        {
            get
            {
                return new Command(() =>
                {
                    App.ServiceLocator.SessionService.ProductosCarrito = null;
                    App.ServiceLocator.SessionService.User = null;
                    App.ServiceLocator.SessionService.Token = null;
                    MainMenuView view = App.ServiceLocator.MainMenuView;
                    view.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProductosView)));
                    view.IsPresented = false;
                    MessagingCenter.Send<MainMenuView>
                 (App.ServiceLocator.MainMenuView, "RELOADLOGIN");
                });
            }
        }
    }
}
