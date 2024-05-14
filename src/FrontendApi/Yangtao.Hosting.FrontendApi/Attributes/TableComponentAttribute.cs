namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableComponentAttribute : Attribute
    {
        public Type? QueryForm { set; get; }

        public Type? AddForm { set; get; }

        public Type? EditForm { set; get; }
    }
}
