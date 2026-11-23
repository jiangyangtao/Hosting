using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 批量删除
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BatchDeleteActionAttribute : HttpActionAttributeBase, IAction
    {
        public BatchDeleteActionAttribute(Type? requestData, string? actionApi) : base(actionApi, Enums.HttpMethodType.Delete)
        {
            RequestData = requestData;
        }

        public BatchDeleteActionAttribute(Type? requestData, Type controllerType, string methodName) : base(controllerType, methodName)
        {
            RequestData = requestData;
        }

        public Type? RequestData { get; }
    }
}
