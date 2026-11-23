using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    /// <summary>
    /// 字段基类
    /// </summary>
    internal abstract class FieldBase : IField
    {
        protected FieldBase()
        {
        }

        protected FieldBase(PropertyInfo property)
        {       
            Name = property.Name;
            Text = StaticDocument.DocumentHandler.GetPropertySummary(property);

            var formatAttribute = property.GetCustomAttribute<FormatterAttribute>();
            if (formatAttribute != null) Format = formatAttribute.Formatter;

            var uniqueKey = property.GetCustomAttribute<UniqueKeyAttribute>();
            if (uniqueKey != null) IsKey = true;
        }

        public string? Name { protected set; get; }

        public string? Text { protected set; get; }

        public string? Format { protected set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public abstract FieldType FieldType { get; }

        public bool IsKey { protected set; get; }
    }
}
