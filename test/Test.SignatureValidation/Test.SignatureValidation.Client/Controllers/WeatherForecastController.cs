using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using JsonContent = Yangtao.Hosting.Common.JsonContent;

namespace Test.SignatureValidation.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IClientSignatureValidationProvider _signatureValidationProvider;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IClientSignatureValidationProvider signatureValidationProvider)
        {
            _logger = logger;
            _signatureValidationProvider = signatureValidationProvider;
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

        [HttpPost]
        public async Task<string> Post()
        {
            var httpClient = new HttpClient();
            var content = new { Id = 1, Name = "Alex" };
            var json = JsonConvert.SerializeObject(content);
            var signature = _signatureValidationProvider.SignData(json);
            httpClient.DefaultRequestHeaders.Add("signature", signature);

            var jsonContent = new JsonContent(content);
            await httpClient.PostAsync("http://localhost:5234/WeatherForecast", jsonContent);

            return "1";
        }
    }
}