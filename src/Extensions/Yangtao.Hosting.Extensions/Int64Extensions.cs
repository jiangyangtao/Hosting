
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


        #region 除法扩展


        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, long divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;

            return value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, long? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value / divisor.Value;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, long divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;
            if (value.HasValue == false) return defalutResult;

            return value.Value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, long? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (value.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value.Value / divisor.Value;
        }


        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, long divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;

            defalutResult = value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, long? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value / divisor.Value;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, long divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;
            if (value.HasValue == false) return false;

            defalutResult = value.Value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, long? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (value.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value.Value / divisor.Value;
            return true;
        }






        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, int divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;

            return value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, int? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value / divisor.Value;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, int divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;
            if (value.HasValue == false) return defalutResult;

            return value.Value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, int? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (value.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value.Value / divisor.Value;
        }


        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, int divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;

            defalutResult = value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, int? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value / divisor.Value;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, int divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;
            if (value.HasValue == false) return false;

            defalutResult = value.Value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, int? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (value.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value.Value / divisor.Value;
            return true;
        }







        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, float divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;

            return value / (decimal)divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, float? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value / (decimal)divisor.Value;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, float divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;
            if (value.HasValue == false) return defalutResult;

            return value.Value / (decimal)divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, float? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (value.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value.Value / (decimal)divisor.Value;
        }


        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, float divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;

            defalutResult = value / (decimal)divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, float? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value / (decimal)divisor.Value;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, float divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;
            if (value.HasValue == false) return false;

            defalutResult = value.Value / (decimal)divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, float? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (value.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value.Value / (decimal)divisor.Value;
            return true;
        }





        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, double divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;

            return value / (decimal)divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, double? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value / (decimal)divisor.Value;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, double divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;
            if (value.HasValue == false) return defalutResult;

            return value.Value / (decimal)divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, double? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (value.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value.Value / (decimal)divisor.Value;
        }


        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, double divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;

            defalutResult = value / (decimal)divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, double? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value / (decimal)divisor.Value;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, double divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;
            if (value.HasValue == false) return false;

            defalutResult = value.Value / (decimal)divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, double? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (value.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value.Value / (decimal)divisor.Value;
            return true;
        }





        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, decimal divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;

            return value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long value, decimal? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value / divisor.Value;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, decimal divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;
            if (value.HasValue == false) return defalutResult;

            return value.Value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this long? value, decimal? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (value.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return value.Value / divisor.Value;
        }


        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, decimal divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;

            defalutResult = value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long value, decimal? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value / divisor.Value;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, decimal divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;
            if (value.HasValue == false) return false;

            defalutResult = value.Value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this long? value, decimal? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (value.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = value.Value / divisor.Value;
            return true;
        }


        #endregion
    }
}
