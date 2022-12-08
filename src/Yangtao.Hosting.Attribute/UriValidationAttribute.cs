using System.ComponentModel.DataAnnotations;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UriValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var url = value.ToString();

            ErrorMessage = ErrorMessage.NotNullAndEmpty() ? ErrorMessage : "not the correct http url format";
            try
            {
                var uri = new Uri(url);
                if (uri.Scheme != "http" && uri.Scheme != "https") return new ValidationResult($"{url} {ErrorMessage}");

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult($"{url} {ErrorMessage}");
            }

        }
    }
}