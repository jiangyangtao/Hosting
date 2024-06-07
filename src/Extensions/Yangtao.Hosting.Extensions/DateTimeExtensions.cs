
namespace Yangtao.Hosting.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 获取当天的 00:00:00.000
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetZeroTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取当天的 23:59:59.999
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFullTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static string ToFormatDate(this DateTime dateTime, string format = "yyyy-MM-dd")
        {
            return dateTime.ToString(format);
        }

        public static string ToFormatDateTime(this DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return dateTime.ToString(format);
        }

        public static string ToFormatDate(this DateTime? dateTime, string format = "yyyy-MM-dd") => dateTime.ToFormat(format);

        public static string ToFormatDateTime(this DateTime? dateTime, string format = "yyyy-MM-dd HH:mm:ss") => dateTime.ToFormat(format);

        public static string ToFormat(this DateTime? dateTime, string format)
        {
            if (dateTime.HasValue == false) return string.Empty;

            return dateTime.Value.ToString(format);
        }

        public static long ToUnixTimeSeconds(this DateTime time) => new DateTimeOffset(time).ToUnixTimeSeconds();

        public static long ToUnixTimeMilliseconds(this DateTime time) => new DateTimeOffset(time).ToUnixTimeMilliseconds();

        public static long ToUnixTimeSeconds(this DateTime? time)
        {
            if (time.HasValue == false) return -1;
            return time.Value.ToUnixTimeSeconds();
        }

        public static long ToUnixTimeMilliseconds(this DateTime? time)
        {
            if (time.HasValue == false) return -1;
            return time.Value.ToUnixTimeMilliseconds();
        }
    }
}
