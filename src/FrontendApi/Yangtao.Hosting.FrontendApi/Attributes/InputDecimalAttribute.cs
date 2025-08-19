namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputDecimalAttribute : Attribute
    {
        public object? Step { set; get; } = 0.1;

        public bool Keyboard { set; get; } = true;

        public bool ShowNumberControls { set; get; } = true;

        public bool Bordered { get; } = true;
    }
}
