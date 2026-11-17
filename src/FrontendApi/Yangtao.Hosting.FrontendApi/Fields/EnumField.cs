using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal class EnumField : Field
    {
        public EnumField(PropertyInfo property) : base(FieldType.Enum, property)
        {
            FieldType = FieldType.Enum;
            ValueOptions = property.PropertyType.GetValueOptions();
        }

        public IEnumerable<TextValueOption> ValueOptions { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override FieldType FieldType { get; }
    }
}
