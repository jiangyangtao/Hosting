using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UploadAttribute : HttpActionAttributeBase
    {
        public UploadAttribute(int uploadCount = 1)
        {
            UploadCount = uploadCount;
        }

        public UploadAttribute(string? actionApi = "", HttpMethodType httpActionType = HttpMethodType.Put, int uploadCount = 1) : base(actionApi, httpActionType)
        {
            UploadCount = uploadCount;
        }

        public UploadAttribute(Type controller, string methodName, int uploadCount = 1) : base(controller, methodName)
        {
            UploadCount = uploadCount;
        }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; }
    }
}
