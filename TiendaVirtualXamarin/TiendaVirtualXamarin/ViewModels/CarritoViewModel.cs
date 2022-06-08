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
            this.serviceVentas = serviceVentas;
            this.serviceProductos = serviceProductos;
            this.LoadCarrito();
            MessagingCenter.Subscribe<CarritoViewModel>(this, "RELOAD", (sender) =>
            {
                this.LoadCarrito();            
            });
                        
        }

        private void LoadCarrito()
        {            
            this.Carrito = App.ServiceLocator.SessionService.ProductosCarrito;            
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
                    Carrito.Clear();
                    MessagingCenter.Send<CarritoViewModel>(App.ServiceLocator.CarritoViewModel, "RELOAD");
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
                    int idprod = (int)idproducto;
                    this.Carrito.RemoveAll(x => x.IdProducto == idprod);                   
                    MessagingCenter.Send<CarritoViewModel>(App.ServiceLocator.CarritoViewModel, "RELOAD");
                    await Application.Current.MainPage.DisplayAlert("Eliminado", "Se ha eliminado el producto del carrito", "Ok");
                });
            }
        }

        public Command VaciarCarrito
        {
            get
            {
                return new Command(async () =>
                {                    
                    this.Carrito.Clear();                    
                    await Application.Current.MainPage.DisplayAlert("", "Se ha vaciado el carrito", "Ok");
                    MessagingCenter.Send<CarritoViewModel>(App.ServiceLocator.CarritoViewModel, "RELOAD");
                });
            }
        }
    }
}
