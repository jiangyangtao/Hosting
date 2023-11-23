
namespace Yangtao.Hosting.Extensions
{
    public static class Int64Extensions
    {
        public static long Value(this long? value, long defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return value.Value;
        }

        public static DateTime? ToDateTimeNullableSeconds(this long value)
        {
            if (value < 0) return null;

            return value.ToDateTimeForSeconds();
        }

        public static DateTime? ToDateTimeForSeconds(this long? value)
        {
            if (value.HasValue == false) return null;

            return value.Value.ToDateTimeNullableSeconds();
        }

        public static DateTime ToDateTimeForSeconds(this long value) => DateTimeOffset.FromUnixTimeSeconds(value).LocalDateTime;

        public static DateTime? ToDateTimeNullableMilliseconds(this long value)
        {
            if (value < 0) return null;

            return value.ToDateTimeForMilliseconds();
        }

        public static DateTime? ToDateTimeForMilliseconds(this long? value)
        {
            if (value.HasValue == false) return null;

            return value.Value.ToDateTimeNullableMilliseconds();
        }

        public static DateTime ToDateTimeForMilliseconds(this long value) => DateTimeOffset.FromUnixTimeMilliseconds(value).LocalDateTime;
    }
}
