using Newtonsoft.Json;

namespace Yangtao.Hosting.Extensions
{
    public static class ObjectExtensions
    {
        public static string Serialize(this object value)
        {
            if (value == null) return string.Empty;

            return JsonConvert.SerializeObject(value);
        }
    }
}
