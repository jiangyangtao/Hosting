namespace Yangtao.Hosting.Extensions
{
    public static class DoubleExtensions
    {
        public static decimal ToDecimal(this double value) => Convert.ToDecimal(value);

        public static decimal ToDecimal(this double? value, decimal defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return Convert.ToDecimal(value.Value);
        }



        #region 除法扩展


        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static double Divide(this double value, long divisor, double defalutResult = 0)
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
        public static double Divide(this double value, long? divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, long divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, long? divisor, double defalutResult = 0)
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
        public static bool TryDivide(this double value, long divisor, out double defalutResult)
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
        public static bool TryDivide(this double value, long? divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, long divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, long? divisor, out double defalutResult)
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
        public static double Divide(this double value, int divisor, double defalutResult = 0)
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
        public static double Divide(this double value, int? divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, int divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, int? divisor, double defalutResult = 0)
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
        public static bool TryDivide(this double value, int divisor, out double defalutResult)
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
        public static bool TryDivide(this double value, int? divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, int divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, int? divisor, out double defalutResult)
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
        public static double Divide(this double value, float divisor, double defalutResult = 0)
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
        public static double Divide(this double value, float? divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, float divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, float? divisor, double defalutResult = 0)
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
        public static bool TryDivide(this double value, float divisor, out double defalutResult)
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
        public static bool TryDivide(this double value, float? divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, float divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, float? divisor, out double defalutResult)
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
        public static double Divide(this double value, double divisor, double defalutResult = 0)
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
        public static double Divide(this double value, double? divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, double divisor, double defalutResult = 0)
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
        public static double Divide(this double? value, double? divisor, double defalutResult = 0)
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
        public static bool TryDivide(this double value, double divisor, out double defalutResult)
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
        public static bool TryDivide(this double value, double? divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, double divisor, out double defalutResult)
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
        public static bool TryDivide(this double? value, double? divisor, out double defalutResult)
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
        public static decimal Divide(this double value, decimal divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;

            return (decimal)value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this double value, decimal? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return (decimal)value / divisor.Value;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this double? value, decimal divisor, decimal defalutResult = 0)
        {
            if (divisor == 0) return defalutResult;
            if (value.HasValue == false) return defalutResult;

            return (decimal)value.Value / divisor;
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static decimal Divide(this double? value, decimal? divisor, decimal defalutResult = 0)
        {
            if (divisor.HasValue == false) return defalutResult;
            if (value.HasValue == false) return defalutResult;
            if (divisor.Value == 0) return defalutResult;

            return (decimal)value.Value / divisor.Value;
        }


        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this double value, decimal divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;

            defalutResult = (decimal)value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this double value, decimal? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = (decimal)value / divisor.Value;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this double? value, decimal divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor == 0) return false;
            if (value.HasValue == false) return false;

            defalutResult = (decimal)value.Value / divisor;
            return true;
        }

        /// <summary>
        /// 尝试除以
        /// </summary>
        /// <param name="value"></param>
        /// <param name="divisor"></param>
        /// <param name="defalutResult"></param>
        /// <returns></returns>
        public static bool TryDivide(this double? value, decimal? divisor, out decimal defalutResult)
        {
            defalutResult = 0;
            if (divisor.HasValue == false) return false;
            if (value.HasValue == false) return false;
            if (divisor.Value == 0) return false;

            defalutResult = (decimal)value.Value / divisor.Value;
            return true;
        }


        #endregion
    }
}
