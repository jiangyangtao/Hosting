namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FormUploadAttribute : Attribute
    {
        public FormUploadAttribute(string? text, string? name)
        {
            Text = text;
            Name = name;
        }

        public string? Text { set; get; }

        public string? Name { set; get; }

        public bool Required { get; }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; } = true;

        public int SortIndex { get; }

        public string? GroupName { get; }
    }
}
