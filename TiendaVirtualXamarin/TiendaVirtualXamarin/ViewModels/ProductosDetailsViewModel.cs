using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;

namespace TiendaVirtualXamarin.ViewModels
{
    public class ProductosDetailsViewModel : ViewModelBase
    {
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
    }
}
