using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
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
        private readonly IClientEncryptionValidationProvider _clientEncryptionValidationProvider;
        private readonly ClientSignatureValidationGrpcProvider.ClientSignatureValidationGrpcProviderClient _grpcProviderClient;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IClientSignatureValidationProvider signatureValidationProvider,
            ClientSignatureValidationGrpcProvider.ClientSignatureValidationGrpcProviderClient grpcProviderClient,
            IClientEncryptionValidationProvider clientEncryptionValidationProvider)
        {
            _logger = logger;
            _signatureValidationProvider = signatureValidationProvider;
            _grpcProviderClient = grpcProviderClient;
            _clientEncryptionValidationProvider = clientEncryptionValidationProvider;
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
            //var signature = _signatureValidationProvider.SignData(json);
            var ciphertext = _clientEncryptionValidationProvider.Encrypt(json);
            //httpClient.DefaultRequestHeaders.Add("signature", signature);

            var jsonContent = new StringContent(ciphertext, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await httpClient.PostAsync("http://localhost:5234/WeatherForecast", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        [HttpPut]
        public async Task<string> Put()
        {
            var request = new LoginRequest()
            {
                Username = "Alex",
                Passwrod = "123456"
            };
            var response = await _grpcProviderClient.LoginAsync(request);

            return "1";
        }
    }
}