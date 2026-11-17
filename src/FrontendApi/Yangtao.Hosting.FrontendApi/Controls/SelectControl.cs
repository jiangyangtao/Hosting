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

        public SelectControl(DictionaryOptionsAttribute dictionaryAttribute, PropertyInfo property) : base(property)
        {
            SelectMode = dictionaryAttribute.SelectMode;
            SourceType = dictionaryAttribute.SourceType;

            if (dictionaryAttribute.ActionApi.NotNullAndEmpty() && dictionaryAttribute.ServiceName.NotNullAndEmpty())
            {
                ActionApi = dictionaryAttribute.ActionApi;
                HttpMethodType = dictionaryAttribute.HttpMethodType;
                ApiVersion = dictionaryAttribute.ApiVersion;
                ServiceName = StaticDocument.DocumentHandler.GetServiceName(dictionaryAttribute.ServiceName);
            }

            if (StaticDocument.DocumentHandler.DictionaryConfig != null && dictionaryAttribute.ActionApi.IsNullOrEmpty() && dictionaryAttribute.ServiceName.IsNullOrEmpty())
            {
                ActionApi = StaticDocument.DocumentHandler.DictionaryConfig.ActionApi;
                ApiVersion = StaticDocument.DocumentHandler.DictionaryConfig.ApiVersion;
                HttpMethodType = StaticDocument.DocumentHandler.DictionaryConfig.HttpMethodType;
                ServiceName = StaticDocument.DocumentHandler.DictionaryConfig.ServiceName;
            }

            Bordered = dictionaryAttribute.Bordered;
            ShowSearch = dictionaryAttribute.ShowSearch;
            AllowClear = dictionaryAttribute.AllowClear;
        }

        private void InitSelectAttribute(SelectAttribute selectAttribute, PropertyInfo property, FieldType fieldType)
        {
            SelectMode = selectAttribute.SelectMode;
            ActionApi = selectAttribute.ActionApi;
            SourceType = selectAttribute.SourceType;
            HttpMethodType = selectAttribute.HttpMethodType;
            Module = selectAttribute.Module;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(selectAttribute.ServiceName);

            Bordered = selectAttribute.Bordered;
            ShowSearch = selectAttribute.ShowSearch;
            AllowClear = selectAttribute.AllowClear;

            if (fieldType == FieldType.Boolean)
            {
                selectAttribute.BooleanTrueText ??= "是";
                selectAttribute.BooleanFalseText ??= "否";
                EnumOptions = new TextValueOption[] { new("true", selectAttribute.BooleanTrueText), new("false", selectAttribute.BooleanFalseText) };
            }

            if (fieldType == FieldType.Enum && selectAttribute.SourceType == SelectSourceType.Enum)
            {
                var type = selectAttribute.Source ?? property.PropertyType;
                EnumOptions = type.GetValueOptions();
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
        public HttpMethodType? HttpMethodType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }

        public string? Module { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Select;
    }
}
