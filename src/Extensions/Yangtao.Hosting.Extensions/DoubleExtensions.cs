namespace Yangtao.Hosting.Extensions
{
    public static class DoubleExtensions
    {
        public static decimal ToDecimal(double value) => Convert.ToDecimal(value);

        public static decimal ToDecimal(double? value, decimal defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return Convert.ToDecimal(value.Value);
        }
    }
}
