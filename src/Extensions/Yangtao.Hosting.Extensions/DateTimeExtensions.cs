
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
            return date.AddDays(0).Date;
        }

        /// <summary>
        /// 获取当天的 23:59:59.999
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFullTime(this DateTime date)
        {
            return date.AddDays(1).Date.AddMilliseconds(-1);
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


        /// <summary>
        /// 获取最后一天
        /// </summary>
        /// <param name="d"></param>
        /// <param name="fullTime">是否覆盖时间</param>
        /// <returns></returns>
        public static DateTime GetLastDay(this DateTime d, bool fullTime = false)
        {
            if (fullTime) return d.AddDays(1 - d.Day).Date.AddMonths(1).AddMilliseconds(-1);

            return d.AddDays(-d.Day).AddMonths(1);
        }

        /// <summary>
        /// 是否为当月最后一天
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool IsLastDay(this DateTime d)
        {
            var lastDay = d.GetLastDay();
            return lastDay.Date == d.Date;
        }

        /// <summary>
        /// 获取第一天
        /// </summary>
        /// <param name="d"></param>
        /// <param name="fullTime">是否覆盖时间</param>
        /// <returns></returns>
        public static DateTime GetFirstDay(this DateTime d, bool fullTime = false)
        {
            var r = d.AddDays(1 - d.Day);
            if (fullTime) return r.Date;

            return r;
        }

        /// <summary>
        /// 是否为当月最后一天
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool IsFirstDay(this DateTime d)
        {
            var lastDay = d.GetFirstDay();
            return lastDay.Date == d.Date;
        }




        /// <summary>
        /// 获取指定时间的时间范围
        /// </summary>
        /// <param name="d">指定时间</param>
        /// <returns></returns>
        public static TimeRange GetTimeRange(this DateTime d) => TimeRange.GetParam(d);

        /// <summary>
        /// 获取指定时间的月份时间范围
        /// </summary>
        /// <param name="d">指定时间</param>
        /// <returns></returns>
        public static TimeRange GetMonthRange(this DateTime d)
        {
            return new()
            {
                BeginTime = d.GetFirstDay(true),
                EndTime = d.GetLastDay(true),
            };
        }
    }

    /// <summary>
    /// 时间范围
    /// </summary>
    public class TimeRange
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }

        public static TimeRange GetToday()
        {
            var now = DateTime.Now;
            return new()
            {
                BeginTime = now.GetZeroTime(),
                EndTime = now.GetFullTime(),
            };
        }

        public static TimeRange GetParam(DateTime d)
        {
            return new()
            {
                BeginTime = d.GetZeroTime(),
                EndTime = d.GetFullTime(),
            };
        }
    }
}
