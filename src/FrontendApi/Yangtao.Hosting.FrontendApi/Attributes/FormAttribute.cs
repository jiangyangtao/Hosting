using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FormAttribute : HttpActionAttributeBase
    {
        public FormAttribute(FormDisplayMode displayMode = FormDisplayMode.Drawer)
        {
            DisplayMode = displayMode;
        }

        public FormAttribute(string? actionApi, HttpMethodType httpActionType, FormDisplayMode displayMode = FormDisplayMode.Drawer) : base(actionApi, httpActionType)
        {
            DisplayMode = displayMode;
        }

        public FormAttribute(Type controller, string methodName, FormDisplayMode displayMode = FormDisplayMode.Drawer) : base(controller, methodName)
        {
            DisplayMode = displayMode;
        }

        public FormDisplayMode DisplayMode { set; get; }

        public override HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Post;
    }
}
