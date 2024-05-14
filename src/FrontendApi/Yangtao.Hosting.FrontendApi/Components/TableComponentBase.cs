using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;

namespace Yangtao.Hosting.FrontendApi.Components
{
    internal abstract class TableComponentBase : ComponentBase, IHttpAction
    {
        protected TableComponentBase(Type tableComponentType, XmlDocumentHandler xmlHandler) : base(tableComponentType, xmlHandler)
        {
            if (tableComponentType.BaseType != null && tableComponentType.BaseType == StaticKeys.PaginationType) IsPagination = true;

            var httpActionAttribute = tableComponentType.GetCustomAttribute<HttpActionAttribute>();
            if (httpActionAttribute != null)
            {
                ActionApi = httpActionAttribute.ActionApi;
                HttpActionType = httpActionAttribute.HttpActionType;
                HttpVersion = httpActionAttribute.HttpVersion;
                ServiceName = httpActionAttribute.ServiceName;
            }

            Fields = tableComponentType.BuildFields(xmlHandler);
        }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpActionType { set; get; } = HttpMethodType.Get;

        public IEnumerable<FieldBase> Fields { set; get; }

        public bool IsPagination { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpVersion HttpVersion { set; get; } = HttpVersion.v1;

        public string? ServiceName { set; get; }
    }
}
