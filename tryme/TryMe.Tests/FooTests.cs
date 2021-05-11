using Xunit;

namespace TryMe.Tests
{
    public class FooTests
    {
        [Fact]
        public void TestBar()
        {
            Assert.Equal("Baz", new Foo().Bar);
        }
    }
}
