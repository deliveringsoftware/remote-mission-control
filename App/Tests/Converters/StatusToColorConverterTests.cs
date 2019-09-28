using AzureDevops.Client.Services.Builds.Models;
using AzureDevops.Support.Converters;
using System;
using Xamarin.Forms;
using Xunit;

namespace AzureDevops.Tests.Converters
{
    public class StatusToColorConverterTests
    {
        [Fact]
        public void StatusToColorConverterArgumentNullException()
        {
            var converter = new StatusToColorConverter();
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, null, null, null));
        }

        [Fact]
        public void StatusToColorConverterArgumentException()
        {
            var converter = new StatusToColorConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert("String", null, null, null));
        }

        [Theory]
        [InlineData(Result.None, "#FFFBFFF7")]
        [InlineData(Result.Canceled, "#FFDCDCDC")]
        [InlineData(Result.PartiallySucceeded, "#FFC1FF33")]
        [InlineData(Result.Failed, "#FFFF0000")]
        [InlineData(Result.Succeeded, "#FF0CBC5F")]
        public void StatusToColorConverterResult(Result result, string expected)
        {
            var converter = new StatusToColorConverter();
            var actual = ((Color)converter.Convert(result, null, null, null)).ToHex();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TaskResult.Abandoned, "#FFC0C0C0")]
        [InlineData(TaskResult.Canceled, "#FFDCDCDC")]
        [InlineData(TaskResult.Failed, "#FFFF0000")]
        [InlineData(TaskResult.Skipped, "#FF96F5E3")]
        [InlineData(TaskResult.SucceededWithIssues, "#FFADFF2F")]
        [InlineData(TaskResult.Succeeded, "#FF0CBC5F")]
        public void StatusToColorConverterTaskResult(TaskResult result, string expected)
        {
            var converter = new StatusToColorConverter();
            var actual = ((Color)converter.Convert(result, null, null, null)).ToHex();
            Assert.Equal(expected, actual);
        }
    }
}