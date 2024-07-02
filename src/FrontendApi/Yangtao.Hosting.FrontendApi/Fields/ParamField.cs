using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal class ParamField : IField
    {
        public ParamField(PropertyInfo property, DocumentHandler documentHandler)
        {
            Name = property.Name;
            Text = documentHandler.GetPropertySummary(property);

            FieldType = property.PropertyType.GetFieldType();

            var formatAttribute = property.GetCustomAttribute<FormatterAttribute>();
            if (formatAttribute != null) Format = formatAttribute.Formatter;

            var uniqueKey = property.GetCustomAttribute<UniqueKeyAttribute>();
            if (uniqueKey != null) IsKey = true;
        }

        public string? Name { protected set; get; }

        public string? Text { protected set; get; }

        public string? Format { protected set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FieldType FieldType { protected set; get; }

        public bool IsKey { protected set; get; }
    }
}
