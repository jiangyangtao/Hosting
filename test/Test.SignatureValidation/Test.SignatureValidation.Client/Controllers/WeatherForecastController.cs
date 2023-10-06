using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.SignatureValidation.ClientGrpc.Provider;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using JsonContent = Yangtao.Hosting.Common.JsonContent;

namespace Test.SignatureValidation.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IClientSignatureValidationProvider _signatureValidationProvider;
        private readonly ClientSignatureValidationGrpcProvider.ClientSignatureValidationGrpcProviderClient _grpcProviderClient;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IClientSignatureValidationProvider signatureValidationProvider,
            ClientSignatureValidationGrpcProvider.ClientSignatureValidationGrpcProviderClient grpcProviderClient)
        {
            _logger = logger;
            _signatureValidationProvider = signatureValidationProvider;
            _grpcProviderClient = grpcProviderClient;
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

        [HttpPut]
        public async Task<string> Put()
        {
            var request = new LoginRequest()
            {
                Username = "Alex",
                Passwrod = "123456"
            };
            var response =await _grpcProviderClient.LoginAsync(request);

            return "1";
        }
    }
}