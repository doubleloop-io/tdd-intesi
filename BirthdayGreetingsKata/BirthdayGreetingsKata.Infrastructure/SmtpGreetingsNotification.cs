using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BirthdayGreetingsKata.Infrastructure
{
    public class SmtpGreetingsNotification : IDisposable
    {
        readonly IPAddress address;
        readonly int port;
        readonly string fromAddress;
        readonly SmtpClient smtpClient;

        public SmtpGreetingsNotification(IPAddress address, int port, string fromAddress)
        {
            this.address = address;
            this.port = port;
            this.fromAddress = fromAddress;
            smtpClient = new SmtpClient(address.ToString(), port);
        }

        public async Task SendForBirthday(Employee employee)
        {
            try
            {
                await smtpClient.SendMailAsync(
                    fromAddress,
                    employee.Email,
                    "Happy birthday!",
                    $"Happy birthday, dear {employee.FirstName}!");
            }
            catch (SmtpException ex)
            {
                throw new GreetingsNotificationException(
                    address.ToString(),
                    port,
                    ex
                );
            }
        }

        public void Dispose()
        {
            smtpClient.Dispose();
        }
    }

    public class GreetingsNotificationException : Exception
    {
        public GreetingsNotificationException(string address, int port, SmtpException innerException)
            : base($"Cannot send email to {address}:{port} endpoint.")
        {
        }
    }
}
