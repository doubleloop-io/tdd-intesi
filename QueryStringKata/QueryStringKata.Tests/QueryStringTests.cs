using Xunit;

namespace QueryStringKata.Tests
{
    public class QueryStringTests
    {
        [Fact]
        public void LookupByKey()
        {
            var qs = new QueryString(new[] {("foo", "bar")});
            var result = qs.Lookup("foo");
            Assert.Equal("bar", result);
        }

        [Fact]
        public void LookupUnknownKey()
        {
            var qs = new QueryString(new[] {("foo", "bar")});
            var result = qs.Lookup("pippo");
            Assert.Null(result);
        }

        [Fact]
        public void LookupInt32ByKey()
        {
            var qs = new QueryString(new[] {("foo", "123")});
            var result = qs.LookupInt32("foo");
            Assert.Equal(123, result);
        }

        [Fact]
        public void DuplicatedKeys()
        {
            var qs = new QueryString(new[] {("foo", "bar"), ("foo", "baz")});
            var result = qs.Lookup("foo");
            Assert.Equal("bar", result);
        }

        [Fact]
        public void Empty()
        {
            var qs = new QueryString(new (string, string)[0]);
            var result = qs.Lookup("foo");
            Assert.Null(result);
        }
    }
}
