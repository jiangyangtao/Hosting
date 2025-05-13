using Yangtao.Hosting.FrontendApi.Attributes;
using Test.FrontApi.Application.Controllers;

namespace Test.FrontApi.Application
{
    [Form(typeof(WeatherForecastController), nameof(WeatherForecastController.Get))]
    [Module(typeof(WeatherForecastController))]
    public class DataDto
    {
        public string? Id { set; get; }

        [Select(BooleanTrueText = "成功", BooleanFalseText = "失败")]
        public bool? IsSuccess { set; get; }
    }
}
