using AzureDevops.Support.Converters;
using System;
using Xunit;

namespace AzureDevops.Tests.Converters
{
    public class DepthToColumnDefinitionWidthConverterTests
    {
        [Fact]
        public void DepthToColumnDefinitionWidthConverterArgumentNullException()
        {
            var converter = new DepthToColumnDefinitionWidthConverter();
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, null, null, null));
        }

        [Fact]
        public void DepthToColumnDefinitionWidthConverterArgumentException()
        {
            var converter = new DepthToColumnDefinitionWidthConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert(new { a = 1 }, null, null, null));
        }

        [Theory]
        [InlineData(3, 36)]
        [InlineData(8, 96)]
        [InlineData(0, 1)]
        public void DepthToColumnDefinitionWidthConverter(int value, int expected)
        {
            var converter = new DepthToColumnDefinitionWidthConverter();
            var actual = converter.Convert(value, null, null, null);
            Assert.Equal(expected, actual);
        }
    }
}