using Microsoft.AspNetCore.Mvc;

namespace Sample.APIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IHelloService helloService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHelloService helloService)
        {
            _logger = logger;
            this.helloService = helloService;
        }

        [HttpGet("hello")]

        public string Hello()
        {
            return helloService.Say();
        }


        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("events")]
        public async Task EventStream()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");

            var messageList = new List<string>
        {
            "message1",
            "message2",
            "message3",
            "message4",
        };
            foreach (var item in messageList)
            {
                var message = $"data:{item}\n\n";
                await Response.WriteAsync(message);
                await Response.Body.FlushAsync();
                await Task.Delay(500);
            }
        }
    }
}