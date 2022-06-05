using System;
using System.Collections.Generic;
using System.Text;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Views;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class PerfilViewModel: ViewModelBase
    {
        public Command MostarCarrito
        {
            get
            {
                return new Command(async () =>
                {
                                        
                });
            }
        }
    }
}
