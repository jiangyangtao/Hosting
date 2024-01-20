
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

        #region 返回 length 对应的 Size

        /// <summary>
        /// 返回 ContentLength 对应的Size
        /// </summary>
        /// <param name="length"> ContentLength 长度</param>
        /// <returns>
        /// B/KB/MB/GB/TB/PB
        /// </returns>
        public static string FileSize(this long length)
        {
            var size = Convert.ToDouble(length);
            var units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            var mod = 1024.0;
            var i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return $"{Math.Round(size)}{units[i]}";
        }

        #endregion

        #region 将此实例的数值转换为其千分位的字符串表示形式

        /// <summary>
        /// 将此实例的数值转换为其千分位的字符串表示形式
        /// </summary>
        /// <param name="value">要转换的<see cref="long"/></param>
        /// <returns>此实例的值的千分位字符串表示形式</returns>
        public static string ToThousand(this long value) => string.Format("{0:N}", value);

        #endregion
    }
}
