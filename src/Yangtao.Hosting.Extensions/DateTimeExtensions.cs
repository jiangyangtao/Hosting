using System;

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

    }
}
