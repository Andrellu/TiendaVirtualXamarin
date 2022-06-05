using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;
using TiendaVirtualXamarin.Views;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class CategoriasViewModel:ViewModelBase
    {
        private ServiceCategoria _ServiceCategoria;
        private ServiceProductos _ServiceProductos;
        public CategoriasViewModel(ServiceCategoria serviceCategoria, ServiceProductos serviceProductos)
        {
            this._ServiceCategoria = serviceCategoria;
            this._ServiceProductos = serviceProductos;

            Task.Run(async () =>
            {
                await this.LoadCategoriasAsync();
            });
        }


        private ObservableCollection<Categoria> _Categorias;
        public ObservableCollection<Categoria> Categorias
        {
            get { return this._Categorias; }
            set
            {
                this._Categorias = value;
                OnPropertyChanged("Categorias");
            }
        }

        public async Task LoadCategoriasAsync()
        {
            List<Categoria> categorias = await this._ServiceCategoria.GetAllCategorias();
            this.Categorias = new ObservableCollection<Categoria>(categorias);
        }

        public Command ShowProductosCategoria
        {
            get
            {
                return new Command(async (categoria) =>
                {
                    List<Producto> data = await this._ServiceProductos.GetProductosCategoriaAsync((string)categoria);
                    ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
                    foreach(Producto p in data)
                    {
                        productos.Add(p);
                    }
                    ProductosCategoriaView view = new ProductosCategoriaView();
                    ProductosCategoriaViewModel viewmodel = App.ServiceLocator.ProductosCategoriaViewModel;
                    viewmodel.Productos = productos;
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
