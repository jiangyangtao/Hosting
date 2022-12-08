using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.Attribute
{
    /// <summary>
    /// 集合元素的校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CollectionValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowNull { set; get; } = false;

        /// <summary>
        /// 最小元素数量
        /// </summary>
        public int Minimum { set; get; } = 0;

        /// <summary>
        /// 最大元素数量
        /// </summary>
        public int Maximum { set; get; } = 0;

        /// <summary>
        /// 相对于当前验证属性验证指定值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                if (AllowNull) return ValidationResult.Success;
                if (AllowNull == false && value == null) return new ValidationResult($"{validationContext.MemberName} 不允许为空");
            }

            var r = value is IEnumerable<dynamic>;
            if (r == false) return new ValidationResult($"{value} 不是正确的集合类型");

            var collection = value as IEnumerable<dynamic>;
            var dynamics = collection.ToArray();
            if (dynamics.Length < Minimum) return new ValidationResult($"{validationContext.MemberName} 最少包含 {Minimum} 个元素");
            if (Maximum > 0 && dynamics.Length > Maximum) return new ValidationResult($"{validationContext.MemberName} 不允许超出 {Maximum} 个元素");

            return ValidationResult.Success;
        }
    }
}
