using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class SelectControl : ControlBase, IHttpAction
    {
        public SelectControl(PropertyInfo property, FieldType fieldType, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            SourceType = SelectSourceType.Enum;
            EnumOptions = property.PropertyType.GetValueOptions(documentHandler);

            var selectAttr = property.GetCustomAttribute<SelectAttribute>();
            if (selectAttr != null) InitSelectAttribute(selectAttr, property, fieldType, documentHandler);
        }

        public SelectControl(SelectAttribute selectAttribute, PropertyInfo property, FieldType fieldType, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            InitSelectAttribute(selectAttribute, property, fieldType, documentHandler);
        }

        private void InitSelectAttribute(SelectAttribute selectAttribute, PropertyInfo property, FieldType fieldType, DocumentHandler documentHandler)
        {
            SelectMode = selectAttribute.SelectMode;
            ActionApi = selectAttribute.ActionApi;
            SourceType = selectAttribute.SourceType;
            HttpActionType = selectAttribute.HttpActionType;
            ServiceName = documentHandler.GetServiceName(selectAttribute.ServiceName);

            Bordered = selectAttribute.Bordered;
            ShowSearch = selectAttribute.ShowSearch;
            AllowClear = selectAttribute.AllowClear;

            if (fieldType == FieldType.Boolean)
            {
                EnumOptions = new TextValueOption[] { new("true", "否"), new("false", "是") };
            }

            if (selectAttribute.SourceType == SelectSourceType.Enum)
            {
                var type = selectAttribute.Source ?? property.PropertyType;
                EnumOptions = type.GetValueOptions(documentHandler);
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
