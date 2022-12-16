using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.Attribute
{
    /// <summary>
    /// 自定义 Swagger 类型及格式的校验特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class SwaggerSchemaValidationAttribute : ValidationAttribute
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
