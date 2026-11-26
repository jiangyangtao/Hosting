namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputDecimalAttribute : Attribute
    {
        public decimal Step { set; get; } = 0.1M;

        public bool Keyboard { set; get; } = true;

        public bool ShowNumberControls { set; get; } = true;

        public bool Bordered { set; get; } = true;
    }
}
