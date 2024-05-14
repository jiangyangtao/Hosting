using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrefixAttribute : Attribute
    {
        public PrefixAttribute(string value)
        {
            Value = value;
        }

        public PrefixAttribute(string value, AffixType affixType) : this(value)
        {
            PrefixType = affixType;
        }

        public string? Value { get; }

        public AffixType PrefixType { get; } = AffixType.Text;
    }
}
