using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
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
            this.CargarMenu();
                MessagingCenter.Subscribe<MainMenuView>
                (this, "RELOADLOGIN", (sender) =>
                {
                    this.CargarMenu();
                });
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

        private void CargarMenu()
        {
            ObservableCollection<MasterPageItem> menu = new ObservableCollection<MasterPageItem>();

            if (App.ServiceLocator.SessionService.User != null)
            {
                //CON LOGIN
                menu = new ObservableCollection<MasterPageItem>
            {
                new MasterPageItem
                {
                    Titulo="Con Login",
                    Tipo=typeof(ProductosView),
                    Icono="home.png"
                },
                new MasterPageItem
                {
                    Titulo="Categorías",
                    Tipo=typeof(CategoriasView),
                    Icono="category.png"
                },
                new MasterPageItem
                {
                    Titulo="Login",
                    Tipo=typeof(LoginView),
                    Icono="loginn.png"
                }
                ,
                new MasterPageItem
                {
                    Titulo="Perfil",
                    Tipo=typeof(PerfilView),
                    Icono="profilee.png"
                }
            };
                this.MenuPrincipal = menu;
            }
            else
            {
                //SIN LOGIN
                menu = new ObservableCollection<MasterPageItem>
            {
                new MasterPageItem
                {
                    Titulo="Sin Login",
                    Tipo=typeof(ProductosView),
                    Icono="home.png"
                },
                new MasterPageItem
                {
                    Titulo="Categorías",
                    Tipo=typeof(CategoriasView),
                    Icono="category.png"
                },
                new MasterPageItem
                {
                    Titulo="Login",
                    Tipo=typeof(LoginView),
                    Icono="loginn.png"
                }
                ,
                new MasterPageItem
                {
                    Titulo="Perfil",
                    Tipo=typeof(PerfilView),
                    Icono="profilee.png"
                }
            };
                this.MenuPrincipal = menu;
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

