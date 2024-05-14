namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FormGroupAttribute : Attribute
    {
        public FormGroupAttribute(string? groupName, int sortIndex)
        {
            SortIndex = sortIndex;
            GroupName = groupName;
        }

        public int SortIndex { set; get; }

        public string? GroupName { set; get; }
    }
}
