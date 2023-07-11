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
       //links:
       //react
       //https://gist.github.com/roshanlabh/eac12ebf9fa76880699256aefcb8d955
       //sql:
       //https://github.com/Hardanish-Singh/CoderByte-Challenges-Solutions/blob/master/SQL/sql_vendor_join.sql
      //https://github.com/Hardanish-Singh/CoderByte-Challenges-Solutions/blob/master/SQL/sql_member_count.sql
      //csharp:
      // https://coderbyte.com/solution/Tree%20Constructor
      //json:
      // https://dotnetfiddle.net/o5RPai
      //kode in csharp:
      
      //longest:
      //using System;
     //using System.Text.RegularExpressions;
     //class MainClass {
 // public static string LongestWord(string sen) { 
      
  
    // code goes here  
    /* Note: In C# the return type of a function and the 
       parameter types being passed are defined, so this return 
       call must match the return type of the function.
       You are free to modify the return type. */
   // Regex reg = new Regex("[^a-zA-Z0-9]");
   // sen = reg.Replace(sen, " ");


   // string longestWord = String.Empty;
  //  string[] arr = sen.Split(" ");
  //  for (int i = 0; i < arr.Length; i++) {
   //     if (arr[i].Length > longestWord.Length)
   //         longestWord = arr[i];
   // }
  //  return longestWord;   
//  }

  //static void Main() {  
    // keep this function call here
  //  Console.WriteLine(LongestWord(Console.ReadLine()));
 // } 
   
//}

//aaabbc:
//using System;
//using System.Linq;
//using System.Text.RegularExpressions;
//class MainClass {
  //aabbbc
//  public static string LongestWord(string str) { 
   // string newStr="";
   // char prev=str[0];
   // int count=1;
    //for(int i=1;i<str.Length;i++){
    

    //if(str[i]!=prev){
    // newStr+=count;
     //    newStr+=prev;
  //   count=1;

   //     }
   //     else
    //    count++;
    
     //   prev=str[i];
     //     if(i==str.Length-1){
     //         newStr+=count;
     //    newStr+=prev;
     //     }
         
      //  }

  //  return newStr;   
 // }

 // static void Main() {  
    // keep this function call here
  //  Console.WriteLine(LongestWord(Console.ReadLine()));
 // } 
   
//}







      


    }
}
