namespace Papaya.Hosting.Extensions
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
    }
}
