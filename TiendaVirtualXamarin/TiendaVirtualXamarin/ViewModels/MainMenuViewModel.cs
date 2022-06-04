using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Code;
using TiendaVirtualXamarin.Views;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class MainMenuViewModel: ViewModelBase
    {
        public MainMenuViewModel()
        {
            ObservableCollection<MasterPageItem> menu = new ObservableCollection<MasterPageItem>
            {
                new MasterPageItem
                {
                    Titulo="Inicio",
                    Tipo=typeof(ProductosView),
                    Icono="home.png"
                },
                new MasterPageItem
                {
                    Titulo="Categorías",
                    Tipo=typeof(CategoriasView),
                    Icono="category.png"
                }
            };
            this.MenuPrincipal = menu;
        }
        private ObservableCollection<MasterPageItem> _MenuPrincipal;
        public ObservableCollection<MasterPageItem> MenuPrincipal
        {
            get { return this._MenuPrincipal; }
            set
            {
                this._MenuPrincipal = value;
                OnPropertyChanged("MenuPrincipal");
            }
        }
        //  Comando para devolver la vista que se ha seleccionado en el menu:
        public Command PaginaSeleccionada
        {
            get
            {
                return new Command((seleccionado) => {
                    MainMenuView masterPage = App.ServiceLocator.MainMenuView;
                    var item = (MasterPageItem)seleccionado;
                    var tipo = item.Tipo;
                    masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(tipo));
                    masterPage.IsPresented = false;
                });
            }
        }
    }
}
