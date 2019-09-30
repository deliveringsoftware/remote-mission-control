using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace AzureDevops.Support.Converters
{
    public class ProjectIdToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return value;

            var colors = new string[]
            {
                    "#FF0000",
                    "#AA0000",
                    "#5C2893",
                    "#DA3A00",
                    "#32105C",
                    "#0075DA",
                    "#BA68C8",
                    "#5C6BC0",
                    "#B600A0",
                    "#004C1A",
            };

            if (value is Guid id)
            {
                var index = GetIndexFromGuid(id);
                return colors[index];
            }
            else
                throw new ArgumentException("Invalid value type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;

        private int GetIndexFromGuid(Guid id)
        {
            var text = id.ToString();
            var numbers = new string(text.Where(Char.IsDigit).ToArray());

            int sum(string values)
            {
                var value = 0;
                foreach (var number in values)
                    value += int.Parse(number.ToString());
                return value;
            }

            var index = sum(numbers);
            while (index >= 10)
                index = sum(index.ToString());

            return index;
        }
    }
}