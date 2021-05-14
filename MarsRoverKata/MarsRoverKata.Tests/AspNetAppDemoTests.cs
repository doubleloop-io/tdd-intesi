using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MarsRoverKata.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MarsRoverKata.Tests
{
    public class AspNetAppDemoTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly WebApplicationFactory<Startup> factory;

        public AspNetAppDemoTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetWeather()
        {
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var response = await client.PostAsJsonAsync("weatherforecast",
                new
                {
                    city = "livorno"
                });

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(body);
        }
    }
}
