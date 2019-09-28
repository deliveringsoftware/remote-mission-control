using AzureDevops.Support.ExtentionMethods;
using System;
using Xunit;

namespace AzureDevops.Tests.ExtentionMethods
{
    public class TimeSpanExtentionMethodsTests
    {
        [Theory]
        [InlineData(-1, "-")]
        [InlineData(01, "1s")]
        [InlineData(59, "59s")]
        public void TimeSpanToTextSeconds(int second, string expected)
        {
            var timeSpan = new TimeSpan(0, 0, 0, second, 32);
            var actual = timeSpan.ToText();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 15, "1M 15s")]
        [InlineData(1, 01, "1M 1s")]
        [InlineData(1, 59, "1M 59s")]
        public void TimeSpanToTextMinutes(int minutes, int second, string expected)
        {
            var timeSpan = new TimeSpan(0, 0, minutes, second, 32);
            var actual = timeSpan.ToText();
            Assert.Equal(expected, actual);
        }
    }
}