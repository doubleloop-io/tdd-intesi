using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarsRoverKata.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        static readonly Dictionary<string, int> Data = new()
        {
            {"LIVORNO", 25},
            {"FIRENZE", 20},
        };

        readonly ILogger<WeatherForecastController> logger;
        readonly Random rng;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
            rng = new Random();
        }

        [HttpPost]
        public WeatherForecastResonse Post([FromBody] WeatherForecastRequest request)
        {
            logger.Log(LogLevel.Trace, "GET called");
            var city = request.City.Trim().ToUpperInvariant();
            return new WeatherForecastResonse
            {
                City = city,
                Date = DateTime.Now,
                TemperatureC = Data[city],
            };
        }
    }
}
