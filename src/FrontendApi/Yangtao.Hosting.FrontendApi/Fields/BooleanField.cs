using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal class BooleanField : FieldBase, IBoolean
    {
        public BooleanField(BooleanFieldAttribute booleanAttribute, PropertyInfo property) : base(property)
        {
            TrueText = booleanAttribute.TrueText;
            FalseText = booleanAttribute.FalseText;
            TagMode = booleanAttribute.TagMode;
        }

        public string? TrueText { set; get; } = "是";

        public string? FalseText { set; get; } = "否";

        public bool TagMode { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public override FieldType FieldType => FieldType.Boolean;
    }
}
