namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TextAreaAttribute : Attribute
    {
        public TextAreaAttribute(int rows = 3)
        {
            Rows = rows;
        }

        public bool ShowCount { set; get; }

        public int Rows { set; get; }

        public bool Bordered { set; get; }

        public bool AllowClear { set; get; } = true;
    }
}
