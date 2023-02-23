using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;
//using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    [EnableCors()]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        static HttpClient httpClient = new HttpClient();
        string path = "http://api.weatherapi.com/v1/forecast.json?key=39f8ecaf506c4f76b3f55139222906&q=";
        //string path3 = "&days=3&api=yes&alert=yes";
       

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


        [HttpGet ("{city}")]
        public async Task<string> GetByCityAsync(string city)
        {
            path += city;
            var get = await httpClient.GetAsync(path);
            var res=await get.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(res);
            var cuerrent =(JObject) json["current"];
            var temp_c = (int)cuerrent["temp_c"];
            var condition = (string)cuerrent["condition"]["text"];

            return$"the weather in {city} is: temp {temp_c} condition {condition}";  
        }
        [HttpGet("{city},{days}")]
        public async Task<string> Get3DaysByCityAsync(string city, int days)
        {
            path += city;
            path += "&days=3&api=yes&alert=yes";
            var get = await httpClient.GetAsync(path);
            var res = await get.Content.ReadAsStringAsync();
            //JObject json = JObject.Parse(res);

            return res;
        }
       

    }
}