using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SelectorComponentAttribute : Attribute
    {
        public SelectorComponentAttribute(Type? queryForm = null)
        {
            QueryForm = queryForm;
        }

        public Type? QueryForm { set; get; }

        public SelectionMode SelectionMode { set; get; } = SelectionMode.Single;
    }
}
