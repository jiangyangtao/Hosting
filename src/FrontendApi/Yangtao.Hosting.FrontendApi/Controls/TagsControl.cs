using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class TagsControl : ControlBase
    {
        public TagsControl(TagsAttribute tagsAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {

        }

        public override ControlType ControlType => ControlType.Tags;
    }
}
