using System;
using System.Net;
using System.Threading.Tasks;
using BirthdayGreetingsKata.Infrastructure;

namespace BirthdayGreetingsKata.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var repo = new CsvEmployeeRepository(args[0]);
            var notification = new SmtpGreetingsNotification(IPAddress.Parse(args[1]), int.Parse(args[2]), args[3]);
            var service = new BirthdayService(repo, notification);

            var today = DateTime.Today;
            if (args.Length == 5)
                today = args[4].ToDate();

            await service.SendGreetings(today);
        }
    }
}
