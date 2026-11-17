using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 导出操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportActionAttribute : HttpActionAttributeBase, IAction
    {

        public Type? RequestData { get; }

        public ExportActionAttribute(string? actionApi, Type? requestData) : base(actionApi, Enums.HttpMethodType.Post)
        {
            RequestData = requestData;
        }

        public ExportActionAttribute(Type controller, string methodName, Type? requestData = null) : base(controller, methodName)
        {
            RequestData = requestData;
        }
    }
}
