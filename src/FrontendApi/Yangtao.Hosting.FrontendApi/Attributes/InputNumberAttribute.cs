namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputNumberAttribute : Attribute
    {
        public int Step { set; get; } = 1;

        public bool Keyboard { set; get; } = true;

        public bool ShowNumberControls { set; get; } = true;

        public bool Bordered { set; get; } = true;
    }
}
