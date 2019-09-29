using AzureDevops.Support.Converters;
using System;
using Xunit;

namespace AzureDevops.Tests.Converters
{
    public class ProjectIdToColorConverterTests
    {
        [Fact]
        public void ProjectIdToColorConverterArgumentNullException()
        {
            var converter = new ProjectIdToColorConverter();
            Assert.Null(converter.Convert(null, null, null, null));
        }

        [Fact]
        public void ProjectIdToColorConverterArgumentException()
        {
            var converter = new ProjectIdToColorConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert(new { a = 1 }, null, null, null));
        }

        [Theory]
        [InlineData("d0ba0aa0-dfae-0d0e-b00d-000da000000f", "#FF0000")]
        [InlineData("d1ba0aa0-dfae-0d0e-b00d-000da000000f", "#AA0000")]
        [InlineData("d1ba1aa0-dfae-0d0e-b00d-000da000000f", "#5C2893")]
        [InlineData("d1ba1aa1-dfae-0d0e-b00d-000da000000f", "#DA3A00")]
        [InlineData("d1ba1aa1-dfae-1d0e-b00d-000da000000f", "#32105C")]
        [InlineData("d1ba1aa1-dfae-1d1e-b00d-000da000000f", "#0075DA")]
        [InlineData("d1ba1aa1-dfae-1d1e-b10d-000da000000f", "#BA68C8")]
        [InlineData("d1ba1aa1-dfae-1d1e-b11d-000da000000f", "#5C6BC0")]
        [InlineData("d1ba1aa1-dfae-1d1e-b11d-100da000000f", "#B600A0")]
        [InlineData("d1ba1aa1-dfae-1d1e-b11d-110da000000f", "#004C1A")]
        [InlineData("d1ba1aa1-dfae-1d1e-b11d-111da000000f", "#AA0000")]
        [InlineData("d1ba1aa1-dfae-1d1e-b11d-111da100000f", "#5C2893")]
        public void ProjectIdToColorConverter(string value, string expected)
        {
            var id = new Guid(value);
            var converter = new ProjectIdToColorConverter();
            var actual = converter.Convert(id, null, null, null);
            Assert.Equal(expected, actual);
        }
    }
}