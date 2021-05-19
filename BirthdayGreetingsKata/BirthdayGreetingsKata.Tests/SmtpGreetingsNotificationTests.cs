using System.Net;
using System.Threading.Tasks;
using BirthdayGreetingsKata.Infrastructure;
using netDumbster.smtp;
using Xunit;

namespace BirthdayGreetingsKata.Tests
{
    /* TEST LIST
      - invia email a John
      - email address inesistente
      - guardia: no name || no email
      - server non raggiungibile
     */
    public class SmtpGreetingsNotificationTests
    {
        [Fact]
        public async Task SendBirthdayEmail()
        {
            // SETUP/CLEANUP
            // start/connect+clear server smtp
            using var server = SimpleSmtpServer.Start(5000);
            using var notification = new SmtpGreetingsNotification(server.Configuration.IPAddress, server.Configuration.Port, "foo@bar.com");

            await notification.SendForBirthday(
                new Employee
                {
                    FirstName = "John",
                    Email = "a@b.com"
                });

            var email = Assert.Single(server.ReceivedEmail);
            Assert.Equal("Happy birthday!", email.Subject);
            var body = email.MessageParts[0].BodyData;
            Assert.Equal("Happy birthday, dear John!", body);
            Assert.Equal("a@b.com", email.ToAddresses[0].Address);

            // TEARDOWN
            // stop/disconnect
        }

        [Fact]
        public async Task ServerUnreachable()
        {
            using var notification = new SmtpGreetingsNotification(IPAddress.Any, 5000, "foo@bar.com");

            var ex = await Record.ExceptionAsync(
                async () =>
                    await notification.SendForBirthday(
                        new Employee
                        {
                            FirstName = "John",
                            Email = "a@b.com"
                        }));

            Assert.IsType<GreetingsNotificationException>(ex);
            Assert.Contains(IPAddress.Any.ToString(), ex.Message);
            Assert.Contains("5000", ex.Message);
        }
    }
}
