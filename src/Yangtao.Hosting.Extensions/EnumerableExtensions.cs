namespace Yangtao.Hosting.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> values) => values == null || values.Count() == 0;

        public static bool NotNullAndEmpty<T>(this IEnumerable<T> values) => values.IsNullOrEmpty() == false;
    }
}
