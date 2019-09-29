using AzureDevops.Client.Services.Projects.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace AzureDevops.Support.Converters
{
    public class ProjectVisibilityToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return value; ;

            if (value is ProjectVisibility visibility)
            {
                if (visibility == ProjectVisibility.Private)
                    return Fonts.FontAwesomeIcons.Lock;
                else
                    return Fonts.FontAwesomeIcons.LockOpen;
            }
            else
                throw new ArgumentException("Invalid value type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}