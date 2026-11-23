using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Controls;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Components
{
    /// <summary>
    /// 表单组件
    /// </summary>
    internal class FormComponent : QueryFormComponent, IHttpAction
    {
        public FormComponent(Type formType, FormAttribute? formAttr = null) : base(formType)
        {
            ServiceName = StaticDocument.DocumentHandler.GetServiceName();

            formAttr ??= formType.GetCustomAttribute<FormAttribute>();
            if (formAttr != null)
            {
                DisplayMode = formAttr.DisplayMode;
                ActionApi = formAttr.ActionApi;
                ApiVersion = formAttr.ApiVersion;
                HttpMethodType = formAttr.HttpMethodType;
            }

            var httpActionAttribute = formType.GetCustomAttribute<HttpActionAttribute>() ?? throw new InvalidOperationException($"{formType.FullName} must specify set HttpAction or config HttpActionAttribute.");

            ActionApi = httpActionAttribute.ActionApi;
            HttpMethodType = httpActionAttribute.HttpMethodType;
            ApiVersion = httpActionAttribute.ApiVersion;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(httpActionAttribute.ServiceName);

            var formUploadAttributes = formType.GetCustomAttributes<FormUploadAttribute>();
            if (formUploadAttributes.NotNullAndEmpty())
            {
                var controls = formUploadAttributes.Select(a => new FormUploadControl(a));
                if (controls.NotNullAndEmpty()) ControlList.AddRange(controls);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormDisplayMode DisplayMode { set; get; }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Post;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }



        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        private bool IsValidHttpAction => ActionApi.NotNullAndEmpty() && ServiceName.NotNullAndEmpty();
    }
}
