using Yangtao.Hosting.Extensions;

namespace TestExtensionsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();

            string? a = null;
            var r = a.IsNullOrEmpty();
            Console.WriteLine(r);
        }
    }
}