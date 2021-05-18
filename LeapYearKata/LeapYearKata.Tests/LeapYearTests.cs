using Xunit;

namespace LeapYearKata.Tests
{
    public class LeapYearTests
    {
        // metedo, eseguito dal test runnerpublic
        // nome metodo: scenario under test
        [Fact]
        public void TypicalLeapYear()
        {
            // A - arrange: prepara lo stato iniziale del sistema
            var year = new Year(2000);
            // A - act: esegui lo use case
            var result = year.IsLeap;
            // A - assert: verifica dell'output
            Assert.True(result);
        }

        [Fact]
        public void TypicalCommonYear()
        {
            var year = new Year(1900);
            var result = year.IsLeap;
            Assert.False(result);
        }

        [Fact]
        public void AtypicalLeapYear()
        {
            var year = new Year(2016);
            var result = year.IsLeap;
            Assert.True(result);
        }

        [Fact]
        public void AtypicalCommonYear()
        {
            var year = new Year(2019);
            var result = year.IsLeap;
            Assert.False(result);
        }
    }
}
