using AzureDevops.Client.Services.Projects.Models;
using AzureDevops.Support.Converters;
using System;
using Xunit;

namespace AzureDevops.Tests.Converters
{
    public class ProjectVisibilityToIconConverterTests
    {
        [Fact]
        public void ProjectVisibilityToIconConverterArgumentNullException()
        {
            var converter = new StatusToColorConverter();
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, null, null, null));
        }

        [Fact]
        public void ProjectVisibilityToIconConverterArgumentException()
        {
            var converter = new StatusToColorConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert("String", null, null, null));
        }

        [Theory]
        [InlineData(ProjectVisibility.Private, "\uf023")]
        [InlineData(ProjectVisibility.Public, "\uf3c1")]
        public void ProjectVisibilityToIconConverter(ProjectVisibility visibility, string expected)
        {
            var converter = new ProjectVisibilityToIconConverter();
            var actual = converter.Convert(visibility, null, null, null);
            Assert.Equal(expected, actual);
        }
    }
}