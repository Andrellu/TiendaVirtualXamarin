using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;

namespace TiendaVirtualXamarin.ViewModels
{
    public class VentasUsuarioViewModel : ViewModelBase
    {
        private ServiceVentas service;

        public VentasUsuarioViewModel(ServiceVentas service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadVentasUsuarioAsync();
            });
        
        }

        private async Task LoadVentasUsuarioAsync()
        {
            Usuario user = App.ServiceLocator.SessionService.User;
            List<Venta> ventas = await this.service.GetVentasUsuario(user.NombreUsuario);
            this.Ventas = new ObservableCollection<Venta>(ventas);
        }

        private ObservableCollection<Venta> _Ventas;

        public ObservableCollection<Venta> Ventas
        {
            get { return this._Ventas; }
            set
            {
                this._Ventas = value;
                OnPropertyChanged("Ventas");
            }
        }

    }
}
