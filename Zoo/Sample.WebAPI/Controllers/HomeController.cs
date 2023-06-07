using Microsoft.AspNetCore.Mvc;
using Zoo.Woody;

namespace Sample.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet( "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("ApiResponseSuccess")]
        public ApiResponse<string> ApiResponseSuccess()
        {
            return ApiResponse.Success("hello", new Pager { PageNo = 1, PageSize = 10, Total = 205 });
        }
        
        [HttpGet("ApiResponseFailure")]
        public ApiResponse<string> ApiResponseFailure()
        {
            return ApiResponse.Failure<string>(4002, "something is error");
        }

        [HttpGet("Error")]
        public IEnumerable<WeatherForecast> Error()
        {
            throw new BusinessException(5000,"something is  error ");
        }
    }
}