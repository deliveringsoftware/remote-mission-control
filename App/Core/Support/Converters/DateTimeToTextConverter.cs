using AzureDevops.Support.ExtentionMethods;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace AzureDevops.Support.Converters
{
    public class DateTimeToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value is DateTime dateTime)
            {
                DateTime? now = null;
                if (parameter is DateTime dateTimeNowToTest)
                    now = dateTimeNowToTest;

                return dateTime.ToText(now);
            }
            else
                throw new ArgumentException("Invalid value type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
