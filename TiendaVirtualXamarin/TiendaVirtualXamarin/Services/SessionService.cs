using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiendaVirtualXamarin.Services
{
    public class SessionService
    {
        public List<Producto> ProductosCarrito { get; set; }        

        public SessionService()
        {
            this.ProductosCarrito = new List<Producto>();
        }
    }
}
