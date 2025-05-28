using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Yangtao.Hosting.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 指示指定的字符串是NULL还是空字符串(“”)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>如果Value参数为空或空字符串(“”)，则为True；否则为False</returns>
        public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// 指示指定的字符串不为NULL且不为空字符串(“”)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>如果Value参数不为NULL且不为空字符串(“”)，则为True；否则为False</returns>
        public static bool NotNullAndEmpty(this string? value) => value.IsNullOrEmpty() == false;

        public static string Value(this string? value, string defaultValue = "")
        {
            if (value == null) return defaultValue;
            if (value.IsNullOrEmpty()) return defaultValue;

            return value;
        }

        public static T? DeserializeToObject<T>(this string value)
        {
            if (value.IsNullOrEmpty()) return default;

            return JsonConvert.DeserializeObject<T>(value);
        }

        public static bool TryDeserializeToObject<T>(this string value, out T? result)
        {
            result = default;
            if (value.IsNullOrEmpty()) return false;

            try
            {
                result = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 指示指定的字符串是否为电子邮箱
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>
        /// 如果字符串为 null 或者空字符串("")，则返回 false;
        /// 如果 value 参数为正确的电子邮箱格式，则返回 true;
        /// 否则返回 false 。
        /// </returns>
        public static bool IsEmail(this string value)
        {
            if (value.IsNullOrEmpty()) return false;

            var emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            var emailReg = new Regex(emailStr);
            return emailReg.IsMatch(value.Trim());
        }

        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns><see cref="true"/> 则有中文，<see cref="false"/> 则没有中文</returns>
        public static bool HasChinese(this string value)
        {
            if (value.IsNullOrEmpty()) return false;

            return Regex.IsMatch(value, @"[\u4e00-\u9fa5]");
        }

        public static bool IsExcel(this string value)
        {
            var suffixName = value[(value.LastIndexOf(".") + 1)..];
            if (suffixName.Equals("xlsx", StringComparison.OrdinalIgnoreCase)) return true;

            return suffixName.Equals("xls", StringComparison.OrdinalIgnoreCase);
        }

        #region 中文数字转换

        /// <summary>
        /// 将中文数字转换成 int
        /// </summary>
        /// <param name="chineseNumerals"></param>
        /// <returns></returns>
        public static int ChineseNumeralsToInt32(this string chineseNumerals)
        {
            var i = chineseNumerals.ChineseNumeralsToInt64();
            return Convert.ToInt32(i);
        }

        /// <summary>
        /// 将中文数字转换成 long
        /// </summary>
        /// <param name="chineseNumerals"></param>
        /// <returns></returns>
        public static long ChineseNumeralsToInt64(this string chineseNumerals)
        {
            chineseNumerals = Regex.Replace(chineseNumerals, "\\s+", "");
            long firstUnit = 1;
            long secondUnit = 1;
            long result = 0;
            for (var i = chineseNumerals.Length - 1; i > -1; --i)
            {
                var tmpUnit = CharToUnit(chineseNumerals[i]);
                if (tmpUnit > firstUnit)
                {
                    firstUnit = tmpUnit;
                    secondUnit = 1;
                    if (i == 0)
                    {
                        result += firstUnit * secondUnit;
                    }
                    continue;
                }

                if (tmpUnit > secondUnit)
                {
                    secondUnit = tmpUnit;
                    continue;
                }

                result += firstUnit * secondUnit * CharToNumber(chineseNumerals[i]);
            }
            return result;
        }

        /// <summary>
        /// 转换数字
        /// </summary>
        private static long CharToNumber(char c)
        {
            return c switch
            {
                '一' => 1,
                '二' => 2,
                '三' => 3,
                '四' => 4,
                '五' => 5,
                '六' => 6,
                '七' => 7,
                '八' => 8,
                '九' => 9,
                '零' => 0,
                _ => (long)-1,
            };
        }

        /// <summary>
        /// 转换单位
        /// </summary>
        private static long CharToUnit(char c)
        {
            return c switch
            {
                '十' => 10,
                '百' => 100,
                '千' => 1000,
                '万' => 10000,
                '亿' => 100000000,
                _ => (long)1,
            };
        }

        #endregion

        #region Decimal

        public static decimal ToDecimal(this string value, decimal defaultValue = 0)
        {
            if (value.IsNullOrEmpty()) return defaultValue;

            var parseResult = decimal.TryParse(value, out decimal d);
            if (parseResult == false) return defaultValue;

            return d;
        }

        public static decimal? ToDecimalNullable(this string value)
        {
            if (value.IsNullOrEmpty()) return null;

            var parseResult = decimal.TryParse(value, out decimal d);
            if (parseResult == false) return null;

            return d;
        }

        #endregion

        /// <summary>
        /// 转换为 MD5
        /// </summary>
        /// <param name="input">要转换的字符串</param>
        /// <returns></returns>
        public static string ToMd5(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = MD5.HashData(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
