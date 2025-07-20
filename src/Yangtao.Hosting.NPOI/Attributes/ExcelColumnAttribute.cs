namespace Yangtao.Hosting.NPOI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        public ExcelColumnAttribute(string name, bool required = false)
        {
            Name = name;
            Required = required;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 是否不能为空
        /// </summary>
        public bool Required { get; }

        /// <summary>
        /// 校验类型
        /// </summary>
        public Type? ValidateType { set; get; }
    }
}
