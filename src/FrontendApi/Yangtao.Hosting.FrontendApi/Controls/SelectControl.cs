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

        public SelectControl(DictionaryOptionsAttribute dictionaryAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            SelectMode = dictionaryAttribute.SelectMode;
            SourceType = dictionaryAttribute.SourceType;

            if (dictionaryAttribute.ActionApi.NotNullAndEmpty() && dictionaryAttribute.ServiceName.NotNullAndEmpty())
            {
                ActionApi = dictionaryAttribute.ActionApi;
                HttpMethodType = dictionaryAttribute.HttpMethodType;
                ApiVersion = dictionaryAttribute.ApiVersion;
                ServiceName = documentHandler.GetServiceName(dictionaryAttribute.ServiceName);
            }

            if (documentHandler.DictionaryConfig != null && dictionaryAttribute.ActionApi.IsNullOrEmpty() && dictionaryAttribute.ServiceName.IsNullOrEmpty())
            {
                ActionApi = documentHandler.DictionaryConfig.ActionApi;
                ApiVersion = documentHandler.DictionaryConfig.ApiVersion;
                HttpMethodType = documentHandler.DictionaryConfig.HttpMethodType;
                ServiceName = documentHandler.DictionaryConfig.ServiceName;
            }

            Bordered = dictionaryAttribute.Bordered;
            ShowSearch = dictionaryAttribute.ShowSearch;
            AllowClear = dictionaryAttribute.AllowClear;
        }

        private void InitSelectAttribute(SelectAttribute selectAttribute, PropertyInfo property, FieldType fieldType, DocumentHandler documentHandler)
        {
            SelectMode = selectAttribute.SelectMode;
            ActionApi = selectAttribute.ActionApi;
            SourceType = selectAttribute.SourceType;
            HttpMethodType = selectAttribute.HttpMethodType;
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
        public HttpMethodType? HttpMethodType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Select;
    }
}
