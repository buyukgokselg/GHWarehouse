using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHWarehouseApp.Converters
{
    public class CardColorMultiConverter : IMultiValueConverter
    {
       
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 ||
                !(values[0] is int scanned) ||
                !(values[1] is int required))
                return Colors.Transparent;

            if (scanned < required)
                return Colors.LightBlue;
            else if (scanned == required)
                return Colors.LightGreen;
            else
                return Colors.Orange;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
