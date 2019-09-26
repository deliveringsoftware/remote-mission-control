using System;

namespace AzureDevops.Support.ExtentionMethods
{
    public static class StringExtentionMethods
    {
        public static string Truncate(this string text, int length, string sufix = "...")
        {
            if (length < 0)
                throw new ArgumentException($"Invalid length: {length}");

            if (string.IsNullOrEmpty(text)) return string.Empty;
            if (length == 0) return text;
            if (text.Length <= length) return text;
            return $"{text.Substring(0, length)}{sufix}";
        }

        public static string Abbreviate(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var parts = text.Split(' ');
            if (parts.Length == 1)
            {
                var abbreviation = string.Empty;
                foreach (var c in text)
                {
                    if (char.IsUpper(c))
                        abbreviation += c.ToString();
                    if (abbreviation.Length == 2) break;
                }

                if (string.IsNullOrEmpty(abbreviation) || abbreviation.Length == 1)
                    abbreviation = text.Substring(0, 2);

                return abbreviation;
            }

            return $"{parts[0][0]}{parts[1][0]}";
        }
    }
}
