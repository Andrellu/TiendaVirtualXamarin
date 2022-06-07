using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class CarritoViewModel:ViewModelBase
    {
        private ServiceVentas serviceVentas;
        private ServiceProductos serviceProductos;

        public CarritoViewModel(ServiceVentas serviceVentas, ServiceProductos serviceProductos)
        {
            this.Carrito = App.ServiceLocator.SessionService.ProductosCarrito;
            this.serviceVentas = serviceVentas;
            this.serviceProductos = serviceProductos;
            //MessagingCenter.Subscribe<CarritoViewModel>(this, "RELOAD", async (sender) =>
            //{
            //    await App.ServiceLocator.SessionService.ProductosCarrito;
            //});
        }

        private List<Producto> _Carrito;

        public List<Producto> Carrito
        {
            get { return this._Carrito; }
            set
            {
                this._Carrito = value;
                OnPropertyChanged("Carrito");
            }
        }

        public Command RealizarCompra
        {
            get
            {
                return new Command(async () =>
                {
                    foreach (Producto p in this.Carrito)
                    {
                        Usuario user = App.ServiceLocator.SessionService.User;
                        Venta venta = new Venta
                        {
                            IdVenta = 0,
                            NombreProducto = p.Nombre,
                            NombreUsuario = user.NombreUsuario,
                            FechaVenta = DateTime.Now,
                            Cantidad = 1,
                            PrecioVenta = p.Precio,
                            RolVenta = user.Rol,
                            Direccion = user.Direccion,
                            Estado = false
                        };
                        await this.serviceVentas.InsertVentaAsync(venta, App.ServiceLocator.SessionService.Token);                        
                    }
                    App.ServiceLocator.SessionService.ProductosCarrito.Clear();
                    await App.Current.MainPage.DisplayAlert("¡ADVERTENCIA!", "Compra realizada con éxito.", "Ok");
                });
            }
        }

        public Command DeleteProductoCarrito
        {
            get
            {
                return new Command(async (idproducto) =>
                {
                    Producto p = await this.serviceProductos.FindProducto((int)idproducto);
                    App.ServiceLocator.SessionService.ProductosCarrito.Remove(p);                    
                    await Application.Current.MainPage.DisplayAlert("Eliminado", "Se ha eliminado el producto del carrito", "Ok");
                });
            }
        }
    }
}
