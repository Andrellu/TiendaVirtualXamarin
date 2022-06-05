using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaVirtualXamarin.Base;

namespace TiendaVirtualXamarin.ViewModels
{
    public class CarritoViewModel:ViewModelBase
    {
        public CarritoViewModel()
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
    }
}
