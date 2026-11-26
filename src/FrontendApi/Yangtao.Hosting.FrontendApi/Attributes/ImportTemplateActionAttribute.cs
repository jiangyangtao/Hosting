using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 导入模板操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ImportTemplateActionAttribute : HttpActionAttributeBase, IAction
    {
        public Type? RequestData { get; }

        public ImportTemplateActionAttribute(string? actionApi, Type? requestData) : base(actionApi, Enums.HttpMethodType.Post)
        {
            RequestData = requestData;
        }

        public ImportTemplateActionAttribute(Type controllerType, string methodName, Type? requestData = null) : base(controllerType, methodName)
        {
            RequestData = requestData;
        }

        public override HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;
    }
}
