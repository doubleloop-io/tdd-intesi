using System;
using System.IO;
using System.Threading.Tasks;
using BirthdayGreetingsKata.WebApp;
using Microsoft.AspNetCore.Mvc.Testing;
using netDumbster.smtp;
using Xunit;

namespace BirthdayGreetingsKata.Tests
{
    public class WebAppTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        const string EmployeesFile = "employees.csv";
        
        readonly WebApplicationFactory<Startup> factory;
        readonly SimpleSmtpServer server;

        public WebAppTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            server = SimpleSmtpServer.Start(5004);
        }

        public void Dispose()
        {
            server.Stop();
            server.Dispose();
        }

        [Fact]
        public async Task SendGreetings()
        {
            await File.WriteAllLinesAsync(
                EmployeesFile,
                new[]
                {
                    "last_name, first_name, date_of_birth, email",
                    "foo, bar, 1982/11/08, a@b.com",
                    "Doe, John, 1982/10/22, john.doe@foobar.com",
                }
            );

            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Add("X-App-Today", "2021/11/08");
            var response = await client.PostAsync("/api/birthday", null);

            response.EnsureSuccessStatusCode();
            Assert.Equal(1, server.ReceivedEmailCount);
        }
    }
}
