namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : Attribute
    {
        public PasswordAttribute(bool canVisible = false)
        {
            CanVisible = canVisible;
        }

        public bool CanVisible { set; get; }

        public bool Bordered { set; get; }
    }
}
