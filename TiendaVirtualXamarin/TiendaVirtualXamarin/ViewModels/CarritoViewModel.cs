using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.Carrito = new ObservableCollection<Producto>(App.ServiceLocator.SessionService.ProductosCarrito);
            int total = 0;
            foreach (Producto p in this.Carrito)
            {
                total += p.Precio;
            }
            this.TotalAComprar = "Comprar Precio Total: " + total + " €";
        }

        private string _TotalAComprar;

        public string TotalAComprar
        {
            get { return this._TotalAComprar; }
            set
            {
                this._TotalAComprar = value;
                OnPropertyChanged("TotalAComprar");
            }
        }


        private ObservableCollection<Producto> _Carrito;

        public ObservableCollection<Producto> Carrito
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
                    if (this.Carrito.Count!=0)
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
                        await App.Current.MainPage.DisplayAlert("¡ENHORABUENA!", "Compra realizada con éxito. Total: " + this.TotalAComprar, "Ok");
                        MessagingCenter.Send<CarritoViewModel>(App.ServiceLocator.CarritoViewModel, "RELOAD");                        
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("¡ERROR!", "Tienes que comprar productos", "Ok");
                    }
                    
                });
            }
        }

        public Command DeleteProductoCarrito
        {
            get
            {
                return new Command((idproducto) =>
                {                    
                    int idprod = (int)idproducto;
                    App.ServiceLocator.SessionService.ProductosCarrito.RemoveAll(x => x.IdProducto == idprod);               
                    MessagingCenter.Send<CarritoViewModel>(App.ServiceLocator.CarritoViewModel, "RELOAD");                    
                });
            }
        }

        public Command VaciarCarrito
        {
            get
            {
                return new Command(async () =>
                {
                    App.ServiceLocator.SessionService.ProductosCarrito.Clear();
                    await Application.Current.MainPage.DisplayAlert("", "Se ha vaciado el carrito", "Ok");
                    MessagingCenter.Send<CarritoViewModel>(App.ServiceLocator.CarritoViewModel, "RELOAD");
                });
            }
        }
    }
}
