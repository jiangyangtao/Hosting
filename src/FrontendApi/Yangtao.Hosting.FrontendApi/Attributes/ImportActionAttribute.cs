using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 导入操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ImportActionAttribute : HttpActionAttributeBase, IAction
    {
        public Type? RequestData { get; }

        public ImportActionAttribute(string? actionApi, Type? requestData) : base(actionApi, Enums.HttpMethodType.Post)
        {
            RequestData = requestData;
        }

        public ImportActionAttribute(Type controller, string methodName, Type? requestData = null) : base(controller, methodName)
        {
            RequestData = requestData;
        }

        public Type? ImportTemplateController { set; get; }

        public string ImportTemplateMethod { set; get; } = string.Empty;

        public bool HasImportTemplate => ImportTemplateController != null && ImportTemplateMethod.NotNullAndEmpty();
    }
}
