using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 前置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AddOnBeforeAttribute : Attribute
    {
        public AddOnBeforeAttribute(string value)
        {
            Value = value;
        }

        public AddOnBeforeAttribute(Type source)
        {
            AddOnBeforeType = AddOnType.Enum;
            AddOnBeforeSource = source;
        }

        public string? Value { get; }

        public AddOnType AddOnBeforeType { get; } = AddOnType.Text;

        public Type? AddOnBeforeSource { get; }
    }
}
