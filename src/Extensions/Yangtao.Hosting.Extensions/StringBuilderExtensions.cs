using System.Text;

namespace Yangtao.Hosting.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendIf(this StringBuilder stringBuilder, Func<bool> func, string value)
        {
            if (stringBuilder == null) throw new ArgumentNullException(nameof(stringBuilder));

            var r = func();
            if (r) stringBuilder.Append(value);

            return stringBuilder;
        }


        public static StringBuilder AppendIf(this StringBuilder stringBuilder, bool result, string value)
        {
            if (stringBuilder == null) throw new ArgumentNullException(nameof(stringBuilder));

            if (result) stringBuilder.Append(value);

            return stringBuilder;
        }
    }
}
