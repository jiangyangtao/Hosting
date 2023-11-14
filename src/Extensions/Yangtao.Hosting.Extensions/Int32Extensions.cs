
namespace Yangtao.Hosting.Extensions
{
    public static class Int32Extensions
    {
        public static int Value(this int? value, int defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return value.Value;
        }
    }
}
