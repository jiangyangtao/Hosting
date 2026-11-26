using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 导出操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportActionAttribute : HttpActionAttributeBase, IAction
    {
        public Type? RequestData { get; }

        public ExportActionAttribute(string? actionApi, Type? requestData) : base(actionApi, Enums.HttpMethodType.Post)
        {
            RequestData = requestData;
        }

        public ExportActionAttribute(Type controllerType, string methodName, Type? requestData = null) : base(controllerType, methodName)
        {
            RequestData = requestData;
        }

        public override HttpMethodType HttpMethodType { get; set; } = HttpMethodType.Get;
    }
}
