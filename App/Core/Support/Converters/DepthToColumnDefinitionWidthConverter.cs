using System;
using System.Globalization;
using Xamarin.Forms;

namespace AzureDevops.Support.Converters
{
    public class DepthToColumnDefinitionWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            const int width = 6;
            if (value is int depth)
            {
                if (depth <= 0)
                    return 1;
                else
                    return width * (depth * 2);
            }
            else
                throw new ArgumentException("Invalid value type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}