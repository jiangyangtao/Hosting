namespace Yangtao.Hosting.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ToRound(this decimal value, int decimals = 2) => Math.Round(value, decimals);

        public static decimal ToRound(this decimal? value, int decimals = 2, decimal defaultValue = 0)
        {
            if (value.HasValue == false) value = defaultValue;

            return Math.Round(value.Value, decimals);
        }

        public static double ToDouble(this decimal value) => Convert.ToDouble(value);

        public static double ToDouble(this decimal? value)
        {
            if (value.HasValue == false) return 0;

            return Convert.ToDouble(value.Value);
        }

        public static double ToDouble(this decimal? value, int decimals = 2)
        {
            if (value.HasValue == false) return 0;

            var d = Math.Round(value.Value, decimals);
            return Convert.ToDouble(d);
        }
    }
}
