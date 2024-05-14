namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputNumberAttribute : Attribute
    {
        public object? Step { set; get; }

        public bool Keyboard { set; get; } = true;

        public bool ShowNumberControls { set; get; } = true;

        public bool Bordered { get; } = true;
    }
}
