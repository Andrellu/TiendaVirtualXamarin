using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;
using TiendaVirtualXamarin.Views;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class CompraProductosViewModel : ViewModelBase
    {
        private ServiceVentas serviceVentas;
        private ServiceProductos serviceProductos;

        private Producto _Producto;
        public Producto Producto
        {
            get { return this._Producto; }
            set
            {
                this._Producto = value;
                OnPropertyChanged("Producto");
            }
        }

        public CompraProductosViewModel(ServiceVentas serviceVentas, ServiceProductos serviceProductos)
        {
            this.serviceVentas = serviceVentas;
            this.serviceProductos = serviceProductos;
        }

        public Command RealizarCompra
        {
            get
            {
                return new Command(async (idProducto) =>
                {
                    Producto producto = await this.serviceProductos.FindProducto((int)idProducto);
                    Usuario user = App.ServiceLocator.SessionService.User;
                    Venta venta = new Venta
                    {
                        IdVenta = 0,
                        NombreProducto = producto.Nombre,
                        NombreUsuario = user.NombreUsuario,
                        FechaVenta = DateTime.Now,
                        Cantidad = 1,
                        PrecioVenta = producto.Precio,
                        RolVenta = user.Rol,
                        Direccion = user.Direccion,
                        Estado = false
                    };
                    await this.serviceVentas.InsertVentaAsync(venta, App.ServiceLocator.SessionService.Token);
                    await App.Current.MainPage.DisplayAlert("¡ADVERTENCIA!", "Compra realizada con éxito.", "Ok");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }
    }
}
