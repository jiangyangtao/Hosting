using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal class EnumField : FieldBase
    {
        public EnumField(PropertyInfo property, XmlDocumentHandler xmlHandler) : base(property, xmlHandler)
        {
            FieldType = FieldType.Enum;
            ValueOptions = property.PropertyType.GetValueOptions(xmlHandler);
        }

        public IEnumerable<TextValueOption> ValueOptions { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override FieldType FieldType { get; }
    }
}
