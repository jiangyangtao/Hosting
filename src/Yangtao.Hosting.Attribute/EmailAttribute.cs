using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Attribute
{
    /// <summary>
    /// 邮箱
    /// </summary>
    public class EmailAttribute : SwaggerSchemaValidationAttribute
    {
        public EmailAttribute()
        {
            Format = "email";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var emailString = value.ToString().Trim();
            if (emailString.IsNullOrEmpty()) return ValidationResult.Success;

            var emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            var isEmail = new Regex(emailStr).IsMatch(emailString);

            ErrorMessage = ErrorMessage.NotNullAndEmpty() ? ErrorMessage : "not the correct mailbox format";
            if (isEmail == false) return new ValidationResult($"{value} {ErrorMessage}");

            return ValidationResult.Success;
        }
    }
}
