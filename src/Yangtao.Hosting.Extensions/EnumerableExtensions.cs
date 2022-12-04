namespace Yangtao.Hosting.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> values) => values == null || values.Any() == false;

        public static bool NotNullAndEmpty<T>(this IEnumerable<T> values) => values.IsNullOrEmpty() == false;

        public static string ToStringValue<T>(this IEnumerable<T> values, string separator = ",")
        {
            if (values.IsNullOrEmpty()) return string.Empty;

            return string.Join(separator, values);
        }
    }
}
