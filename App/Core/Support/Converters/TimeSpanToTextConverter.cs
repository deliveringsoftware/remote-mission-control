using AzureDevops.Support.ExtentionMethods;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace AzureDevops.Support.Converters
{
    public class TimeSpanToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value is TimeSpan timeSpan)
                return timeSpan.ToText();
            else
                throw new ArgumentException("Invalid value type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}