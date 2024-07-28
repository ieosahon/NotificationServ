global using Microsoft.AspNetCore.Mvc;
global using NotificationService.Application.Contract;
global using NotificationService.Application.DTOs.Request;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController()
        {
        
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public  IEnumerable<WeatherForecast> Get()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();

           
            return result;
        }
    }
}