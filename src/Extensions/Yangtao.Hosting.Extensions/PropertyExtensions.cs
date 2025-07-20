using System.Reflection;

namespace Yangtao.Hosting.Extensions
{
    public static class PropertyExtensions
    {
        public static bool HasAttribute<TAttribute>(this PropertyInfo property) where TAttribute : Attribute
        {
            return property.GetCustomAttribute<TAttribute>() != null;
        }
    }
}
