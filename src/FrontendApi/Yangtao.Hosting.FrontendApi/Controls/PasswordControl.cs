using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class PasswordControl : ControlBase, IFieldGroup
    {
        public PasswordControl(PasswordAttribute passwordAttr, PropertyInfo property, XmlDocumentHandler xmlHandler) : base(property, xmlHandler)
        {
            CanVisible = passwordAttr.CanVisible;
            Bordered = passwordAttr.Bordered;
        }

        public bool CanVisible { set; get; } = true;

        public bool Bordered { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Password;
    }
}
