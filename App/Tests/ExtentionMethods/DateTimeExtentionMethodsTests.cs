using AzureDevops.Support.ExtentionMethods;
using System;
using Xunit;

namespace AzureDevops.Tests.ExtentionMethods
{
    public class DateTimeExtentionMethodsTests
    {
        [Fact]
        public void DateTimeGreaterThanNow()
        {
            var now = new DateTime(2019, 05, 11, 12, 00, 00);
            var datetime = new DateTime(2019, 05, 11, 23, 00, 00);
            Assert.Throws<ArgumentException>(() => datetime.ToText(now));
        }

        [Fact]
        public void DateTimeMinValueToText()
        {
            var expected = "-";
            var actual = DateTime.MinValue.ToText();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(11, 00, 00, 01, "4s Ago")]
        [InlineData(11, 00, 00, 04, "1s Ago")]
        [InlineData(10, 23, 59, 55, "10s Ago")]
        [InlineData(10, 23, 59, 06, "59s Ago")]
        public void DateTimeNowToTextSecondsAgo(int day, int hour, int minute, int second, string expected)
        {
            var now = new DateTime(2019, 05, 11, 00, 00, 05);
            var datetime = new DateTime(2019, 05, day, hour, minute, second);
            var actual = datetime.ToText(now);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(12, 00, 01, "29min Ago")]
        [InlineData(12, 00, 25, "5min Ago")]
        [InlineData(11, 23, 59, "31min Ago")]
        [InlineData(11, 23, 31, "59min Ago")]
        public void DateTimeNowToTextMinutesAgo(int day, int hour, int minute, string expected)
        {
            var now = new DateTime(2019, 05, 12, 00, 30, 12);
            var datetime = new DateTime(2019, 05, day, hour, minute, 00);
            var actual = datetime.ToText(now);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(12, 03, "2h Ago")]
        [InlineData(12, 00, "5h Ago")]
        [InlineData(11, 23, "6h Ago")]
        [InlineData(11, 06, "23h Ago")]
        public void DateTimeNowToTextHoursAgo(int day, int hour, string expected)
        {
            var now = new DateTime(2019, 05, 12, 05, 04, 23);
            var datetime = new DateTime(2019, 05, day, hour, 00, 00);
            var actual = datetime.ToText(now);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(7, "Tuesday")]
        [InlineData(8, "Wednesday")]
        [InlineData(9, "Thursday")]
        public void DateTimeNowToTextWeekAgo(int day, string expected)
        {
            var now = new DateTime(2019, 05, 12, 05, 04, 23);
            var datetime = new DateTime(2019, 05, day, 05, 00, 00);
            var actual = datetime.ToText(now);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(05, 06, "6d Ago")]
        [InlineData(05, 01, "11d Ago")]
        [InlineData(04, 23, "19d Ago")]
        [InlineData(04, 12, "30d Ago")]
        public void DateTimeNowToTextDaysAgo(int month, int day, string expected)
        {
            var now = new DateTime(2019, 05, 12, 05, 04, 23);
            var datetime = new DateTime(2019, month, day, 05, 00, 00);
            var actual = datetime.ToText(now);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2019, 01, "4M Ago")]
        [InlineData(2019, 03, "2M Ago")]
        [InlineData(2018, 06, "11M Ago")]
        [InlineData(2018, 12, "5M Ago")]
        public void DateTimeNowToTextMonthsAgo(int year, int month, string expected)
        {
            var now = new DateTime(2019, 05, 12, 05, 04, 23);
            var datetime = new DateTime(year, month, 12, 05, 00, 00);
            var actual = datetime.ToText(now);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2018, "1y Ago")]
        [InlineData(2017, "2y Ago")]
        [InlineData(2016, "3y Ago")]
        public void DateTimeNowToTextYearsAgo(int year, string expected)
        {
            var now = new DateTime(2019, 05, 12, 05, 04, 23);
            var datetime = new DateTime(year, 05, 12, 05, 00, 00);
            var actual = datetime.ToText(now);
            Assert.Equal(expected, actual);
        }
    }
}