﻿using ModelsTVX;
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
    public class ProductosListViewModel : ViewModelBase
    {
        private ServiceProductos service;

        public ProductosListViewModel(ServiceProductos service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadProductosAsync();
            });
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
            List<Producto> data = await this.service.GetProductosAsync();
            int num = data.Count;
            this.Productos = new ObservableCollection<Producto>(data);
        }

        public Command ShowProducto
        {
            get
            {
                return new Command(async (idProducto) =>
                {
                    Producto producto = await this.service.FindProducto((int)idProducto);
                    ProductosDetailsView view = new ProductosDetailsView();
                    ProductosDetailsViewModel viewModel = App.ServiceLocator.ProductosDetailsViewModel;
                    viewModel.Producto = producto;
                    view.BindingContext = viewModel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
