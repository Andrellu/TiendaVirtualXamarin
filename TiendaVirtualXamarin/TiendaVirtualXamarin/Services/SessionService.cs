using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiendaVirtualXamarin.Services
{
    public class SessionService
    {
        public List<Producto> ProductosCarrito { get; set; }
        public List<Producto> ProductosCategoria { get; set; }

        public Usuario User { get; set; }

        public String Token { get; set; }
        public SessionService()
        {
            this.ProductosCarrito = new List<Producto>();
            this.ProductosCategoria = new List<Producto>();
        }
    }
}
