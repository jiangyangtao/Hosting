using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 文件上传
    /// <para>用于有字段的文件上传</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UploadAttribute : HttpActionAttributeBase
    {
        public UploadAttribute(string? actionApi = "", HttpMethodType httpActionType = HttpMethodType.Post, int uploadCount = 1) : base(actionApi, httpActionType)
        {
            UploadCount = uploadCount;
        }

        public UploadAttribute(Type controllerType, string methodName, int uploadCount = 1) : base(controllerType, methodName)
        {            
            UploadCount = uploadCount;
        }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; } = true;

        public override HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Post;
    }
}
