using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UploadAttribute : HttpActionAttributeBase
    {
        public UploadAttribute(string? actionApi = "", HttpMethodType httpActionType = HttpMethodType.Put, int uploadCount = 1) : base(actionApi, httpActionType)
        {
            UploadCount = uploadCount;
        }

        public UploadAttribute(Type controllerType, string methodName, int uploadCount = 1) : base(controllerType, methodName)
        {
            UploadCount = uploadCount;
        }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; }
    }
}
