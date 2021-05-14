using System;

namespace MarsRoverKata.Web
{
    public class WeatherForecastRequest
    {
        public string City { get; set; }
    }

    public class WeatherForecastResonse
    {
        public DateTime Date { get; set; }
        public string City { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
    }
}
