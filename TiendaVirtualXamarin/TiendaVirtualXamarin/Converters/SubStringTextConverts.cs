using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TiendaVirtualXamarin.Converters
{
    public class SubStringTextConverts:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            var s = (string)value;
            var data = char.ToUpper(s[0]) + s.Substring(1);
            if (s.Length>16)
            {
                return data.Substring(0, 16) + "...";
            }
            else
            {
                return data;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        { 
            throw new NotImplementedException(); 
        }
    }
}
