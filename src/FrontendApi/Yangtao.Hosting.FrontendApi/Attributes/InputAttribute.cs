namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputAttribute : Attribute
    {
        public bool ShowCount { set; get; } = true;

        public bool AllowClear { set; get; } = true;

        public bool Bordered { get; } = true;
    }
}
