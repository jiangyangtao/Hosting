using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class RadioGroupControl : ControlBase
    {
        public RadioGroupControl(RadioGroupAttribute radioGroupAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {

        }

        public RadioGroupControl(DictionaryAttribute dictionaryAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {

        }

        public RadioGroupType RadioGroupType { set; get; }

        public override ControlType ControlType => ControlType.RadioGroup;
    }
}
