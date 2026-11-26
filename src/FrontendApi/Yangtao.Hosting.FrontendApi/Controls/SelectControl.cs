using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class SelectControl : ControlBase, IHttpAction
    {
        public SelectControl(PropertyInfo property, FieldType fieldType) : base(property)
        {
            SourceType = SelectSourceType.Enum;
            EnumOptions = property.PropertyType.GetValueOptions();

            var selectAttr = property.GetCustomAttribute<SelectAttribute>();
            if (selectAttr != null) InitSelectAttribute(selectAttr, property, fieldType);
        }

        public SelectControl(SelectAttribute selectAttribute, PropertyInfo property, FieldType fieldType) : base(property)
        {
            InitSelectAttribute(selectAttribute, property, fieldType);
        }

        private void InitSelectAttribute(SelectAttribute selectAttribute, PropertyInfo property, FieldType fieldType)
        {
            SourceType = selectAttribute.SourceType;
            Bordered = selectAttribute.Bordered;
            ShowSearch = selectAttribute.ShowSearch;
            AllowClear = selectAttribute.AllowClear;

            if (selectAttribute.SourceType == SelectSourceType.API)
            {
                if (selectAttribute.ActionApi.IsNullOrEmpty()) throw new InvalidOperationException($"{property.Name} the SourceType is API, must config http action.");

                SelectMode = selectAttribute.SelectMode;
                ActionApi = selectAttribute.ActionApi;
                ApiVersion = selectAttribute.ApiVersion;
                HttpMethodType = selectAttribute.HttpMethodType;
                ServiceName = StaticDocument.DocumentHandler.GetServiceName(selectAttribute.ServiceName);
            }

            if (selectAttribute.SourceType == SelectSourceType.Enum)
            {
                if (fieldType != FieldType.Enum && fieldType != FieldType.Boolean) throw new InvalidOperationException($"{property.Name} the SourceType is Enum, only supports Enum or Boolean.");
                if (fieldType == FieldType.Boolean)
                {
                    selectAttribute.TrueText ??= "是";
                    selectAttribute.FalseText ??= "否";
                    EnumOptions = new TextValueOption[] { new("true", selectAttribute.TrueText), new("false", selectAttribute.FalseText) };
                }

                if (fieldType == FieldType.Enum && selectAttribute.SourceType == SelectSourceType.Enum)
                {
                    var type = selectAttribute.Source ?? property.PropertyType;
                    EnumOptions = type.GetValueOptions();
                }
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
        public HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Select;
    }
}
