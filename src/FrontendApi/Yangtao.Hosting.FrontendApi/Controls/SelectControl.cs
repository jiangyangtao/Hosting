using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class SelectControl : ControlBase, IFieldGroup, IHttpAction
    {
        public SelectControl(PropertyInfo property, XmlDocumentHandler xmlHandler) : base(property, xmlHandler)
        {
            SourceType = SelectSourceType.Enum;
            EnumOptions = property.PropertyType.GetValueOptions(xmlHandler);
        }

        public SelectControl(SelectAttribute selectAttribute, PropertyInfo property, XmlDocumentHandler xmlHandler) : base(property, xmlHandler)
        {
            SelectMode = selectAttribute.SelectMode;
            ActionApi = selectAttribute.ActionApi;
            SourceType = selectAttribute.SourceType;
            HttpActionType = selectAttribute.HttpActionType;
            ServiceName = selectAttribute.ServiceName;

            Bordered = selectAttribute.Bordered;
            ShowSearch = selectAttribute.ShowSearch;
            AllowClear = selectAttribute.AllowClear;

            if (selectAttribute.SourceType == SelectSourceType.Enum)
            {
                var type = selectAttribute.EnumSource ?? property.PropertyType;
                EnumOptions = type.GetValueOptions(xmlHandler);
            }
        }


        public bool AllowClear { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectMode SelectMode { set; get; }

        public bool ShowSearch { set; get; } = true;

        public bool Bordered { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectSourceType SourceType { set; get; }

        public IEnumerable<TextValueOption> EnumOptions { set; get; } = TextValueOption.Empty();

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpActionType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpVersion HttpVersion { set; get; } = HttpVersion.v1;

        public string? ServiceName { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Select;
    }
}
