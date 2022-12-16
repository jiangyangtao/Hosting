using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace Yangtao.Hosting.Common
{
    public class JsonContent : StringContent
    {
        public JsonContent(object data) : base(JsonConvert.SerializeObject(data), Encoding.UTF8, MediaTypeNames.Application.Json)
        { }
    }

    public class JsonContent<TData> : StringContent where TData : class
    {
        public JsonContent(TData data) : base(JsonConvert.SerializeObject(data), Encoding.UTF8, MediaTypeNames.Application.Json)
        { }
    }
}
