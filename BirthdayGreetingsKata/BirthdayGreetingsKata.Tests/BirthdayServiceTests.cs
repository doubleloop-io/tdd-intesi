using System.Collections.Generic;
using System.Threading.Tasks;
using BirthdayGreetingsKata.Infrastructure;
using Xunit;

namespace BirthdayGreetingsKata.Tests
{
    public class BirthdayServiceTests
    {
        [Fact]
        public async Task SendOneGreetings()
        {
            var repository = new StubEmployeeRepository();
            repository.OnLoadReturns(
                new Employee { FirstName = "foo", Email = "a@b.com", DateOfBirth = "1982/05/19".ToDate()},
                new Employee { FirstName = "bar", Email = "z@x.com", DateOfBirth = "2000/10/21".ToDate()}
            );
            var notification = new SpyGreetingsNotification();
            var service = new BirthdayService(repository, notification);

            await service.SendGreetings("2021/05/19".ToDate());

            var employee = Assert.Single(notification.EmployeeNotified);
            Assert.Equal("foo", employee.FirstName);
        }

        class SpyGreetingsNotification : IGreetingsNotification
        {
            public List<Employee> EmployeeNotified { get; } = new List<Employee>();

            public Task SendForBirthday(Employee employee)
            {
                EmployeeNotified.Add(employee);
                return Task.CompletedTask;
            }
        }

        class StubEmployeeRepository : IEmployeeRepository
        {
            IEnumerable<Employee> employees = new List<Employee>();

            public Task<IEnumerable<Employee>> Load() =>
                Task.FromResult(employees);

            public void OnLoadReturns(params Employee[] employees)
            {
                this.employees = employees;
            }
        }
    }
}
