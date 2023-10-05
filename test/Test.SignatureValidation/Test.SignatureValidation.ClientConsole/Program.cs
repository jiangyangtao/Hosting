namespace Test.SignatureValidation.ClientConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var http = new HttpClient();
            await http.GetAsync("https://localhost:7251/WeatherForecast");

            Console.WriteLine("Hello, World!");
        }
    }
}