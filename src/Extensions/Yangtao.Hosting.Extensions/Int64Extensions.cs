
namespace Yangtao.Hosting.Extensions
{
    public static class Int64Extensions
    {
        public static long Value(this long? value, long defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return value.Value;
        }

        public static DateTime ToDateTimeForSeconds(this long value) => DateTimeOffset.FromUnixTimeSeconds(value).LocalDateTime;

        public static DateTime ToDateTimeForMilliseconds(this long value) => DateTimeOffset.FromUnixTimeMilliseconds(value).LocalDateTime;
    }
}
