using System;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayGreetingsKata
{
    /*
     * 1. Extract interface from concrete infrastructure class
     * 2. Move interface (port) into CORE
     * 3. (optional) All infrastructure classes should implements the interface
     * 4. Reduce coupling in application services classes
     * 5. Extract parameter of new infrastructure class
     * 6. Reduce coupling in application services ctor
     */
    public class BirthdayService
    {
        readonly IEmployeeRepository repository;
        readonly IGreetingsNotification notification;

        public BirthdayService(IEmployeeRepository repository, IGreetingsNotification notification)
        {
            this.repository = repository;
            this.notification = notification;
        }

        public async Task SendGreetings(DateTime today)
        {
            var employees = await repository.Load();
            var birthdays = employees.Where(x => x.IsBirthday(today));
            foreach (var birthday in birthdays)
                await notification.SendForBirthday(birthday);
        }
    }
}
