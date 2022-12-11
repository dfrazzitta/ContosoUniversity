using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApi.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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


        [HttpGet(Name = "GetStudents")]
        public string GetStudents()
        {
            List<Student> ls = new List<Student>();

            for (int y = 0; y < 20; y++)
            {
                Student st = new Student();
               // st.FullName = "dave" + y.ToString();
                st.LastName = "fraz" + y.ToString();
                st.EnrollmentDate = DateTime.Now;
                st.FirstMidName = "bulls" + y.ToString();
                

                ls.Add(st);
            }
            var json = JsonSerializer.Serialize(ls);
            return json;
        }
    }
}