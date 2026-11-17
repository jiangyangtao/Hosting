using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class UploadControl : FormUploadControl, IHttpAction
    {
        public UploadControl(UploadAttribute uploadAttribute, PropertyInfo property) : base(property)
        {
            UploadCount = uploadAttribute.UploadCount;
            Bordered = uploadAttribute.Bordered;
            ActionApi = uploadAttribute.ActionApi;
            HttpMethodType = uploadAttribute.HttpMethodType;
            ApiVersion = uploadAttribute.ApiVersion;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(uploadAttribute.ServiceName);
        }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpMethodType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
