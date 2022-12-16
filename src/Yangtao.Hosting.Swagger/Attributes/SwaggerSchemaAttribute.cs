
namespace Yangtao.Hosting.Attribute
{
    /// <summary>
    /// 自定义 Swagger 类型及格式的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerSchemaAttribute : System.Attribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { set; get; }

        /// <summary>
        /// 格式
        /// </summary>
        public string Format { set; get; }
    }
}
