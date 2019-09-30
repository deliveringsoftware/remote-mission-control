using AzureDevops.Support.Converters;
using System;
using Xunit;

namespace AzureDevops.Tests.Converters
{
    public class DateTimeToTextConverterTests
    {
        [Fact]
        public void DateTimeToTextConverterArgumentNullException()
        {
            var converter = new DateTimeToTextConverter();
            Assert.Null(converter.Convert(null, null, null, null));
        }

        [Fact]
        public void DateTimeToTextConverterArgumentException()
        {
            var converter = new DateTimeToTextConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert(new { a = 1 }, null, null, null));
        }

        [Fact]
        public void DateTimeToTextConverter()
        {
            var dateTime = new DateTime(2019, 05, 11, 12, 00, 00);
            var now = new DateTime(2019, 05, 11, 12, 5, 00);
            var converter = new DateTimeToTextConverter();
            var actual = converter.Convert(dateTime, null, now, null);
            Assert.Equal("5min Ago", actual);
        }
    }
}