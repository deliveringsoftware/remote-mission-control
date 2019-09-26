using System;

namespace AzureDevops.Support.ExtentionMethods
{
    public static class DateTimeExtentionMethods
    {
        public static string ToText(this DateTime dateTime, DateTime? dateTimeToTest = null)
        {
            var now = DateTime.Now;
            if (dateTimeToTest != null)
                now = dateTimeToTest.Value;

            if (dateTime == DateTime.MinValue)
                return "-";

            if (dateTime > now)
                throw new ArgumentException($"Invalid datetime values. DateTime: {dateTime}. DateTime Now: {now}");

            var diff = (now - dateTime);

            if (diff.TotalSeconds < 60)
                return $"{Math.Round(diff.TotalSeconds)}s Ago";
            else if (diff.TotalMinutes < 60)
                return $"{Math.Round(diff.TotalMinutes)}min Ago";
            else if (diff.TotalHours < 24)
                return $"{Math.Round(diff.TotalHours)}h Ago";
            else if (diff.TotalDays < 6)
                return dateTime.DayOfWeek.ToString();
            else if (diff.TotalDays >= 6 && diff.TotalDays < 31)
                return $"{Math.Round(diff.TotalDays)}d Ago";
            else
            {
                var totalYears = diff.TotalDays / 360;
                if (totalYears >= 1)
                    return $"{Math.Round(totalYears)}y Ago";
                else
                    return $"{Math.Round(totalYears * 12, MidpointRounding.AwayFromZero)}M Ago";
            }
        }
    }
}