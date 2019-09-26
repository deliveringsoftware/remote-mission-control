using System;
using Xunit;
using AzureDevops.Support.Converters;

namespace AzureDevops.Tests.Converters
{
    public class TextTruncateConverterTests
    {
        [Fact]
        public void TextTruncateConverterArgumentException()
        {
            var converter = new TextTruncateConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert(new { a = 1 }, null, null, null));
        }

        [Fact]
        public void TimeSpanToTextConverterArgumentNullException()
        {
            var converter = new TextTruncateConverter();
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, null, null, null));
        }

        [Fact]
        public void TimeSpanToTextConverterArgumentExceptionParameter()
        {
            var converter = new TextTruncateConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert("Text", null, new { a = 1 }, null));
        }

        [Fact]
        public void TimeSpanToTextConverter()
        {
            var converter = new TextTruncateConverter();
            var expected = "Text...";
            var actual = converter.Convert("Text test", null, 4, null);
            Assert.Equal(expected, actual);
        }
    }
}
