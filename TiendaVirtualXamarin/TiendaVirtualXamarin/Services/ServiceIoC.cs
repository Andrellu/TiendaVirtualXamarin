using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TiendaVirtualXamarin.ViewModels;
using TiendaVirtualXamarin.Views;

namespace TiendaVirtualXamarin.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //REGISTRAMOS TODO LO QUE VAYAMOS A INYECTAR
            builder.RegisterType<ServiceProductos>();
            builder.RegisterType<ServiceCategoria>();
            builder.RegisterType<MainMenuView>().SingleInstance();
            builder.RegisterType<MainMenuViewModel>();
            builder.RegisterType<CategoriasViewModel>();
            builder.RegisterType<ProductosDetailsViewModel>();
            builder.RegisterType<ProductosListViewModel>();
            string resourceName = "TiendaVirtualXamarin.appsettings.json";
            Stream stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
            IConfiguration configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Register<IConfiguration>(x => configuration);
            this.container = builder.Build();
        }

        //CREAMOS LAS PROPIEDADES PARA RECUPERAR LOS VIEWMODELS 
        //DENTRO DE LAS VISTAS
        public ProductosListViewModel ProductosListViewModel
        {
            get
            {
                return this.container.Resolve<ProductosListViewModel>();
            }
        }
        public MainMenuView MainMenuView
        {
            get
            {
                return this.container.Resolve<MainMenuView>();
            }
        }
        public ProductosDetailsViewModel ProductosDetailsViewModel
        {
            get
            {
                return this.container.Resolve<ProductosDetailsViewModel>();
            }
        }
        public MainMenuViewModel MainMenuViewModel
        {
            get
            {
                return this.container.Resolve<MainMenuViewModel>();
            }
        }
        public CategoriasViewModel CategoriasViewModel
        {
            get
            {
                return this.container.Resolve<CategoriasViewModel>();
            }
        }
    }
}
