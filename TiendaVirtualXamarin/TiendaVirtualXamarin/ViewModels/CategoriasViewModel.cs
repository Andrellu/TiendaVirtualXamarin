using ModelsTVX;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtualXamarin.Base;
using TiendaVirtualXamarin.Services;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.ViewModels
{
    public class CategoriasViewModel:ViewModelBase
    {
        private ServiceCategoria _ServiceCategoria;
        public CategoriasViewModel(ServiceCategoria serviceCategoria)
        {
            this._ServiceCategoria = serviceCategoria;

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
    }
}
