using AzureDevops.Support.ExtentionMethods;
using System;
using Xunit;

namespace AzureDevops.Tests.ExtentionMethods
{
    public class StringExtentionMethodsTests
    {
        [Fact]
        public void StringTruncateInvalidLength()
        {
            Assert.Throws<ArgumentException>(() => "Text".Truncate(-15));
        }

        [Theory]
        [InlineData("", 10, "")]
        [InlineData("Text", 0, "Text")]
        [InlineData("Text", 120, "Text")]
        [InlineData("My Text", 3, "My ...")]
        public void StringTruncate(string value, int length, string expected)
        {
            var actual = value.Truncate(length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Angelo Belchior", "AB")]
        [InlineData("unit testing", "ut")]
        [InlineData("AbcDna", "AD")]
        [InlineData("AbcdE Teste", "AT")]
        public void Abbreviate(string value, string expected)
        {
            var actual = value.Abbreviate();
            Assert.Equal(expected, actual);
        }
    }
}