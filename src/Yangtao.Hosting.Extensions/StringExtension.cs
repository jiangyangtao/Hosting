using Newtonsoft.Json;
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
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// 指示指定的字符串不为NULL且不为空字符串(“”)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>如果Value参数不为NULL且不为空字符串(“”)，则为True；否则为False</returns>
        public static bool NotNullAndEmpty(this string value) => value.IsNullOrEmpty() == false;

        public static T Deserialize<T>(this string value)
        {
            if (value.IsNullOrEmpty()) return default;

            return JsonConvert.DeserializeObject<T>(value);
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
    }
}
