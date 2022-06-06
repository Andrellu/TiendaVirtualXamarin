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
    public class ProductosCategoriaViewModel:ViewModelBase
    {
        private ServiceProductos _ServiceProductos;
        public ProductosCategoriaViewModel(ServiceProductos serviceProductos, SessionService serviceSession)
        {
            this._ServiceProductos = serviceProductos;
            this.Productos = new ObservableCollection<Producto>(serviceSession.ProductosCategoria);
            this.Categoria = serviceSession.CategoriaProductos;
        }

        private string _Categoria;
        public string Categoria
        {
            get { return this._Categoria; }
            set
            {
                this._Categoria = value;
                OnPropertyChanged("Categoria");
            }
        }
        private ObservableCollection<Producto> _Productos;
        public ObservableCollection<Producto> Productos
        {
            get { return this._Productos; }
            set
            {
                this._Productos = value;
                OnPropertyChanged("Productos");
            }
        }


        private async Task LoadProductosAsync()
        {
            List<Producto> data = await this._ServiceProductos.GetProductosAsync();
            int num = data.Count;
            this.Productos = new ObservableCollection<Producto>(data);
        }

        public Command ShowProducto
        {
            get
            {
                return new Command(async (idProducto) =>
                {
                    Producto producto = await this._ServiceProductos.FindProducto((int)idProducto);
                    ProductosDetailsView view = new ProductosDetailsView();
                    ProductosDetailsViewModel viewModel = App.ServiceLocator.ProductosDetailsViewModel;
                    viewModel.Producto = producto;
                    view.BindingContext = viewModel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }

        public Command AddCarrito
        {
            get
            {
                return new Command(async (idProducto) =>
                {
                    List<Producto> carrito = App.ServiceLocator.SessionService.ProductosCarrito;
                    Producto p = await this._ServiceProductos.FindProducto((int)idProducto);
                    carrito.Add(p);
                    await Application.Current.MainPage.DisplayAlert("ALERT", "Se añadio el Producto", "OK");
                });
            }
        }
    }
}
