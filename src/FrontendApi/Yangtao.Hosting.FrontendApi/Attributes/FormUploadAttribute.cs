namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 表单上传
    /// <para>用于无字段的文件上传</para>
    /// </summary>
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

        public bool Required { set; get; }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; } = true;
    }
}
