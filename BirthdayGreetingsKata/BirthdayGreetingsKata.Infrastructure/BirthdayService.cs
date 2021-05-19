using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BirthdayGreetingsKata.Infrastructure
{
    public class BirthdayService
    {
        readonly CsvEmployeeRepository repository;
        readonly SmtpGreetingsNotification notification;

        public BirthdayService(
            string file,
            IPAddress smtpServerAddress,
            int smtpServerPort,
            string fromAddress)
        {
            repository = new CsvEmployeeRepository(file);
            notification = new SmtpGreetingsNotification(smtpServerAddress, smtpServerPort, fromAddress);
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
