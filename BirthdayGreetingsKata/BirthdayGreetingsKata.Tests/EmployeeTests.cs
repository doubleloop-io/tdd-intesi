using BirthdayGreetingsKata.Infrastructure;
using Xunit;

namespace BirthdayGreetingsKata.Tests
{
    /*
     * TEST LIST:
     * - nato 19/05/1982, oggi 19/05/2021, true
     * - nato 19/05/1982, oggi 1/10/2021, false
     * - nato 29/02/2021, 28/02/2021, true
     */
    public class EmployeeTests
    {
        [Fact]
        public void IsBirthday()
        {
            var employee = new Employee {DateOfBirth = "1982/05/19".ToDate()};
            Assert.True(employee.IsBirthday("2021/05/19".ToDate()));
        }

        [Fact]
        public void IsNotBirthday()
        {
            var employee = new Employee {DateOfBirth = "1982/05/19".ToDate()};
            Assert.False(employee.IsBirthday("2021/10/01".ToDate()));
        }
    }
}
