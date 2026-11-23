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

        public FormAttribute(Type controllerType, string methodName, FormDisplayMode displayMode = FormDisplayMode.Drawer) : base(controllerType, methodName)
        {
            DisplayMode = displayMode;
        }

        public FormDisplayMode DisplayMode { set; get; }
    }
}
