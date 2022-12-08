using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredEnumAttribute : RequiredAttribute
    {
        public RequiredEnumAttribute(Type enumType)
        {
            EnumType = enumType;
        }

        public Type EnumType { get; }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = Enum.IsDefined(EnumType, value);
                if (result == false)
                {
                    ErrorMessage = $"{value} not {EnumType.Name} the value, it's must be [{string.Join(",", Enum.GetNames(EnumType))}] of one";
                    return false;
                }
            }

            return true;
        }
    }
}
