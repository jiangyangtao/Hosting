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
        public UploadControl(UploadAttribute uploadAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            UploadCount = uploadAttribute.UploadCount;
            Bordered = uploadAttribute.Bordered;
            ActionApi = uploadAttribute.ActionApi;
            HttpActionType = uploadAttribute.HttpActionType;
            HttpVersion = uploadAttribute.HttpVersion;
            ServiceName = documentHandler.GetServiceName(uploadAttribute.ServiceName);
        }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpActionType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion HttpVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
