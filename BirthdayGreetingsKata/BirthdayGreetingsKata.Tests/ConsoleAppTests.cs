using System;
using System.IO;
using System.Threading.Tasks;
using netDumbster.smtp;
using Xunit;

namespace BirthdayGreetingsKata.Tests
{
    public class ConsoleAppTests : IDisposable
    {
        readonly SimpleSmtpServer server;

        public ConsoleAppTests()
        {
            server = SimpleSmtpServer.Start(5003);
        }

        public void Dispose()
        {
            server.Stop();
            server.Dispose();
        }

        [Fact]
        public async Task SendGreetings()
        {
            var file = nameof(SendGreetings);
            await File.WriteAllLinesAsync(
                file,
                new[]
                {
                    "last_name, first_name, date_of_birth, email",
                    "foo, bar, 1982/11/08, a@b.com",
                    "Doe, John, 1982/10/22, john.doe@foobar.com",
                }
            );

            await ConsoleApp.Program.Main(
                new[]
                {
                    file,
                    server.Configuration.IPAddress.ToString(),
                    server.Configuration.Port.ToString(),
                    "sender@z.com",
                    "2021/11/08"
                });

            Assert.Equal(1, server.ReceivedEmailCount);
        }
    }
}
