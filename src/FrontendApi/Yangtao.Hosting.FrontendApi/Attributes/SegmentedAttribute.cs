using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 分段控制器
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SegmentedAttribute : HttpActionAttributeBase
    {
        public bool Block { set; get; } = false;

        /// <summary>
        /// 源类型
        /// <para>Enum 只能用于类型为枚举类型的字段，无须配置 http action</para>
        /// <para>API 必须配置 http action，字段类型无限制</para>
        /// </summary>
        public SelectSourceType SourceType { set; get; } = SelectSourceType.API;

        public override HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;
    }
}
