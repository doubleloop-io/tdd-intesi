using System;
using Xunit;

namespace QueryStringKata.Tests
{
    public class QueryStringParsingTests
    {
        [Fact]
        public void SingleParameter()
        {
            var qs = QueryString.Parse("foo=bar");
            Assert.Equal("bar", qs.Lookup("foo"));
        }

        [Fact]
        public void ManyParameters()
        {
            var qs = QueryString.Parse("foo=bar&pippo=pluto");
            Assert.Equal("bar", qs.Lookup("foo"));
            Assert.Equal("pluto", qs.Lookup("pippo"));
        }

        [Fact]
        public void DuplicatedKeys()
        {
            var qs = QueryString.Parse("foo=bar&foo=pluto");
            Assert.Equal("bar", qs.Lookup("foo"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Empty(string input)
        {
            var qs = QueryString.Parse(input);
            Assert.True(qs.IsEmpty);
        }

        [Theory]
        [InlineData("foo=bar%20baz")]
        [InlineData("foo=bar+baz")]
        public void ValuesWithSpaces(string input)
        {
            var qs = QueryString.Parse(input);
            Assert.Equal("bar baz", qs.Lookup("foo"));
        }

        [Theory]
        [InlineData("foo bar=baz")]
        [InlineData("foo=bar baz")]
        public void InvalidInput(string input)
        {
            var ex = Record.Exception(() => QueryString.Parse(input));
            Assert.IsType<ArgumentException>(ex);
            Assert.Contains(input, ex.Message);
        }
    }
}
