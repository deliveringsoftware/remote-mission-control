using AzureDevops.Support.Converters;
using System;
using Xunit;

namespace AzureDevops.Tests.Converters
{
    public class ProjectNameToAbbreviationConverterTests
    {
        [Fact]
        public void ProjectNameToAbbreviationConverterArgumentNullException()
        {
            var converter = new ProjectNameToAbbreviationConverter();
            Assert.Null(converter.Convert(null, null, null, null));
        }

        [Fact]
        public void ProjectNameToAbbreviationConverterArgumentException()
        {
            var converter = new ProjectNameToAbbreviationConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert(new { a = 1 }, null, null, null));
        }

        [Theory]
        [InlineData("Angelo Belchior", "AB")]
        [InlineData("AbcdE Teste", "AT")]
        public void ProjectNameToAbbreviationConverter(string text, string expected)
        {
            var converter = new ProjectNameToAbbreviationConverter();
            var actual = converter.Convert(text, null, null, null);
            Assert.Equal(expected, actual);
        }
    }
}