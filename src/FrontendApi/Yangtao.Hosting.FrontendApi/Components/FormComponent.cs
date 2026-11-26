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
        /// <summary>
        /// 独立注册，无须指定 <see cref="HttpActionAttribute"/>
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="formAttr"></param>
        public FormComponent(Type formType, FormAttribute formAttr) : base(formType)
        {
            SetHttpAction(formAttr);
            DisplayMode = formAttr.DisplayMode;

            InitFormComponent(formType);

            if (ActionApi.IsNullOrEmpty()) throw new InvalidOperationException($"{formType} must specify set HttpAction or config FormAttribute.");
        }

        /// <summary>
        /// 内部注册，无 <see cref="FormAttribute"/>，必须指定 <see cref="HttpActionAttribute"/>
        /// </summary>
        /// <param name="formType"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public FormComponent(Type formType) : base(formType)
        {
            var httpActionAttribute = formType.GetCustomAttribute<HttpActionAttribute>(false) ?? throw new InvalidOperationException($"{formType.FullName} must config HttpActionAttribute.");
            SetHttpAction(httpActionAttribute);
            InitFormComponent(formType);
        }

        /// <summary>
        /// 初始化表单的文件上传
        /// </summary>
        /// <param name="formType"></param>
        private void InitFormComponent(Type formType)
        {
            var formUploadAttributes = formType.GetCustomAttributes<FormUploadAttribute>(false);
            if (formUploadAttributes.NotNullAndEmpty())
            {
                var controls = formUploadAttributes.Select(a => new FormUploadControl(a));
                if (controls.NotNullAndEmpty()) ControlList.AddRange(controls);
            }
        }

        private void SetHttpAction(IHttpAction httpAction)
        {
            if (ActionApi.NotNullAndEmpty()) return;

            ActionApi = httpAction.ActionApi;
            HttpMethodType = httpAction.HttpMethodType;
            ApiVersion = httpAction.ApiVersion;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(httpAction.ServiceName);
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormDisplayMode DisplayMode { set; get; }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Post;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
