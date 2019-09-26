using System;

namespace AzureDevops.Support.ExtentionMethods
{
    public static class TimeSpanExtentionMethods
    {
        public static string ToText(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds <= 0)
                return "-";
            if (timeSpan.TotalSeconds < 60)
                return $"{timeSpan.Seconds}s";
            else if (timeSpan.TotalMinutes > 1 && timeSpan.TotalMinutes <= 59)
                return $"{timeSpan.Minutes}M {timeSpan.Seconds}s";
            else
                return $"{timeSpan.TotalHours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";
        }
    }
}