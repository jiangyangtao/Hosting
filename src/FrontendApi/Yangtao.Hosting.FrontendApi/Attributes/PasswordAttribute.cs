namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : Attribute
    {
        public bool CanVisible { set; get; } = true;

        public bool Bordered { set; get; } = true;
    }
}
