using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SuffixAttribute : Attribute
    {
        public SuffixAttribute(string value)
        {
            Value = value;
        }

        public SuffixAttribute(string value, AffixType affixType) : this(value)
        {
            PrefixType = affixType;
        }

        public string? Value { get; }

        public AffixType PrefixType { get; } = AffixType.Text;
    }
}
