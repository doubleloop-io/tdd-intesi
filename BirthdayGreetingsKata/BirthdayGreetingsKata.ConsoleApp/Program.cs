using System;
using System.Net;
using BirthdayGreetingsKata.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace BirthdayGreetingsKata.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = BuildConfiguration();

            var repo = new CsvEmployeeRepository(args[0]);
            var notification = new SmtpGreetingsNotification(IPAddress.Parse(args[1]), int.Parse(args[2]), args[3]);

            Console.WriteLine("Hello World!");
        }

        static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
    }
}
