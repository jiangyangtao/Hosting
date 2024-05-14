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
    internal class FormComponent : QueryFormComponent, IHttpAction
    {
        public FormComponent(Type formType, XmlDocumentHandler xmlHandler) : base(formType, xmlHandler)
        {
            var formAttr = formType.GetCustomAttribute<FormAttribute>();
            if (formAttr != null)
            {
                DisplayMode = formAttr.DisplayMode;
                ActionApi = formAttr.ActionApi;
                HttpActionType = formAttr.HttpActionType;
                ServiceName = formAttr.ServiceName;
            }

            var httpActionAttr = formType.GetCustomAttribute<HttpActionAttribute>();
            if (httpActionAttr != null)
            {
                ActionApi = httpActionAttr.ActionApi;
                HttpActionType = httpActionAttr.HttpActionType;
                HttpVersion = httpActionAttr.HttpVersion;
            }

            FormGroups = Array.Empty<IFieldGroup>();

            var groupAttributes = formType.GetCustomAttributes<FormGroupAttribute>();
            if (groupAttributes.NotNullAndEmpty()) FormGroups = groupAttributes.Select(a => new FormGroup(a));

            var formUpdateAttributes = formType.GetCustomAttributes<FormUploadAttribute>();
            if (formUpdateAttributes.NotNullAndEmpty())
            {
                var controls = formUpdateAttributes.Select(a => new FormUploadControl(a));
                if (controls.NotNullAndEmpty()) ControlList.AddRange(controls);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormDisplayMode DisplayMode { set; get; }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpActionType { set; get; }

        public IEnumerable<IFieldGroup> FormGroups { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpVersion HttpVersion { set; get; } = HttpVersion.v1;

        public string? ServiceName { set; get; }
    }

    internal class FormGroup : IFieldGroup
    {
        public FormGroup(FormGroupAttribute formGroup)
        {
            GroupName = formGroup.GroupName;
            SortIndex = formGroup.SortIndex;
        }

        public string? GroupName { set; get; }

        public int SortIndex { set; get; }
    }
}
