using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class TextAreaControl : ControlBase, IFieldGroup
    {
        public TextAreaControl(TextAreaAttribute textAreaAttr, PropertyInfo property, XmlDocumentHandler xmlHandler) : base(property, xmlHandler)
        {
            Rows = textAreaAttr.Rows;
            ShowCount = textAreaAttr.ShowCount;
            Bordered = textAreaAttr.Bordered;
            AllowClear = textAreaAttr.AllowClear;

            var maxLenthAttr = property.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLenthAttr != null) MaxLength = maxLenthAttr.Length;
        }

        public int MaxLength { set; get; } = 200;

        public bool ShowCount { set; get; } = true;

        public bool AllowClear { set; get; } = true;

        public bool Bordered { set; get; } = true;

        public int Rows { set; get; } = 3;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.TextArea;
    }
}
