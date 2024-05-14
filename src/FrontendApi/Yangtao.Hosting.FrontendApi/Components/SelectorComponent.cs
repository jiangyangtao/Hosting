using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Components
{
    internal class SelectorComponent : TableComponentBase
    {
        public SelectorComponent(Type selectorType, XmlDocumentHandler xmlHandler) : base(selectorType, xmlHandler)
        {
            var selectorComponentAttribute = selectorType.GetCustomAttribute<SelectorComponentAttribute>() ??
                throw new ArgumentException($"{nameof(selectorType)} not exist {nameof(SelectorComponentAttribute)}");

            if (selectorComponentAttribute.QueryForm != null)
            {
                QueryForm = new(selectorComponentAttribute.QueryForm, xmlHandler);
                SelectionMode = selectorComponentAttribute.SelectionMode;
            }
        }

        public QueryFormComponent? QueryForm { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectionMode SelectionMode { set; get; } = SelectionMode.Single;
    }
}
