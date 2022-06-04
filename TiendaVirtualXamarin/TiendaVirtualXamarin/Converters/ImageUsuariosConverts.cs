using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.Converters
{
    public class ImageUsuariosConverts : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string picture = value as string;
            return "https://storagetiendavirtualaof.blob.core.windows.net/usuarios/" + picture;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
