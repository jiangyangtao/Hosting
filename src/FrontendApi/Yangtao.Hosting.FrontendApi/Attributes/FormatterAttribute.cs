namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatterAttribute : Attribute
    {
        public FormatterAttribute(string formatter)
        {
            Formatter = formatter;
        }

        public string? Formatter { get; }
    }
}
