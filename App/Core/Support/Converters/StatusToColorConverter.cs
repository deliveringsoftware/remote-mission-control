using AzureDevops.Client.Services.Builds.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace AzureDevops.Support.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if(!(value is Result) && !(value is TaskResult))
                throw new ArgumentException("Invalid value type");

            var colors = new Dictionary<string, Color>
            {
                ["_"] = Color.FromHex("#FFFFFF"),
                ["None"] = Color.FromHex("#FBFFF7"),
                ["Abandoned"] = Color.FromHex("#C0C0C0"),
                ["Skipped"] = Color.FromHex("#96F5E3"),
                ["Failed"] = Color.FromHex("#FF0000"),
                ["Canceled"] = Color.FromHex("#DCDCDC"),
                ["Succeeded"] = Color.FromHex("#0CBC5F"),
                ["SucceededWithIssues"] = Color.FromHex("#ADFF2F"),
                ["PartiallySucceeded"] = Color.FromHex("#C1FF33"),
            };

            switch (value)
            {
                case Result r when r == Result.None: return colors["None"];
                case Result r when r == Result.Canceled: return colors["Canceled"];
                case Result r when r == Result.PartiallySucceeded: return colors["PartiallySucceeded"];
                case Result r when r == Result.Failed: return colors["Failed"];
                case Result r when r == Result.Succeeded: return colors["Succeeded"];
                case TaskResult r when r == TaskResult.Abandoned: return colors["Abandoned"];
                case TaskResult r when r == TaskResult.Canceled: return colors["Canceled"];
                case TaskResult r when r == TaskResult.Failed: return colors["Failed"];
                case TaskResult r when r == TaskResult.Skipped: return colors["Skipped"];
                case TaskResult r when r == TaskResult.SucceededWithIssues: return colors["SucceededWithIssues"];
                case TaskResult r when r == TaskResult.Succeeded: return colors["Succeeded"];
                default: return colors["_"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
