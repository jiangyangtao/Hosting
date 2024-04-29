namespace Yangtao.Hosting.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ToRound(this decimal value, int decimals = 2) => Math.Round(value, decimals);

        public static decimal? ToRound(this decimal? value, int decimals = 2)
        {
            if (value.HasValue == false) return null;

            return Math.Round(value.Value, decimals);
        }

        public static double ToDouble(this decimal? value, int decimals = 2)
        {
            if (value.HasValue == false) return 0;

            return (double)Math.Round(value.Value, decimals);
        }
    }
}
