namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 表格组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableComponentAttribute : Attribute
    {
        public Type? QueryForm { set; get; }

        public Type? AddForm { set; get; }

        public Type? EditForm { set; get; }
    }
}
