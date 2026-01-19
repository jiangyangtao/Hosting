
namespace Yangtao.Hosting.Extensions
{
    public static class Int32Extensions
    {
        public static int Value(this int? value, int defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return value.Value;
        }


        #region 除法扩展


        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this int value, long divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int value, long? divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, long divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, long? divisor, decimal defalutResult = 0)
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
        public static bool TryDivide(this int value, long divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int value, long? divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, long divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, long? divisor, out decimal defalutResult)
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
        public static decimal Divide(this int value, int divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int value, int? divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, int divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, int? divisor, decimal defalutResult = 0)
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
        public static bool TryDivide(this int value, int divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int value, int? divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, int divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, int? divisor, out decimal defalutResult)
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
        public static decimal Divide(this int value, double divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int value, double? divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, double divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, double? divisor, decimal defalutResult = 0)
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
        public static bool TryDivide(this int value, double divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int value, double? divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, double divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, double? divisor, out decimal defalutResult)
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
        public static decimal Divide(this int value, decimal divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int value, decimal? divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, decimal divisor, decimal defalutResult = 0)
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
        public static decimal Divide(this int? value, decimal? divisor, decimal defalutResult = 0)
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
        public static bool TryDivide(this int value, decimal divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int value, decimal? divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, decimal divisor, out decimal defalutResult)
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
        public static bool TryDivide(this int? value, decimal? divisor, out decimal defalutResult)
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
