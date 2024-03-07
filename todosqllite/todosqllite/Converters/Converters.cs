using System;
using System.Globalization;
using Xamarin.Forms;

namespace todosqllite.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value; // Or return false if you expect all values to be boolean
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value; // Or throw an exception if you don't expect to use ConvertBack
        }


    }
}
