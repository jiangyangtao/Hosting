using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 后置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AddOnAfterAttribute : Attribute
    {
        public AddOnAfterAttribute(string value)
        {
            Value = value;
        }

        public AddOnAfterAttribute(Type source)
        {
            AddOnAfterType = AddOnType.Enum;
            AddOnAfterSource = source;
        }


        public string? Value { get; }

        public AddOnType AddOnAfterType { get; } = AddOnType.Text;

        public Type? AddOnAfterSource { get; }
    }
}
