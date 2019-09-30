using AzureDevops.Support.ExtentionMethods;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace AzureDevops.Support.Converters
{
    public class ProjectNameToAbbreviationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return value;

            if (value is string name && !string.IsNullOrEmpty(name))
                return name.Abbreviate();
            else
                throw new ArgumentException("Invalid value type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}