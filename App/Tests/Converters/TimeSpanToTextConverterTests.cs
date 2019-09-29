using AzureDevops.Support.Converters;
using System;
using Xunit;

namespace AzureDevops.Tests.Converters
{
    public class TimeSpanToTextConverterTests
    {
        [Fact]
        public void TimeSpanToTextConverterArgumentException()
        {
            var converter = new TimeSpanToTextConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert("text", null, null, null));
        }

        [Fact]
        public void TimeSpanToTextConverterArgumentNullException()
        {
            var converter = new TimeSpanToTextConverter();
            Assert.Null(converter.Convert(null, null, null, null));
        }

        [Fact]
        public void TimeSpanToTextConverter()
        {
            var converter = new TimeSpanToTextConverter();
            var timespan = new TimeSpan(0, 0, 0, 1, 0);
            var expected = "1s";
            var actual = converter.Convert(timespan, null, null, null);
            Assert.Equal(expected, actual);
        }
    }
}