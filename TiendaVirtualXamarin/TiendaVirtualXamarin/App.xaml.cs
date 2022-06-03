using System;
using TiendaVirtualXamarin.Services;
using TiendaVirtualXamarin.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiendaVirtualXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //  Implementacion del menu lateral en nuestro 'MainPage':
            MainMenuView mainMenu = App.ServiceLocator.MainMenuView;
            mainMenu.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
            MainPage = mainMenu;
        }
        //  Implementacion de las propiedades del 'ServiceIoC' en 'App.xaml.cs':
        private static ServiceIoC _ServiceLocator;
        public static ServiceIoC ServiceLocator
        {
            get
            {
                return _ServiceLocator = _ServiceLocator ?? new ServiceIoC();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
