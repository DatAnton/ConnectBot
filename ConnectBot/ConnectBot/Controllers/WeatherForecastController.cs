using Microsoft.AspNetCore.Mvc;

namespace ConnectBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController()
        {

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return "hello";
        }
    }
}
