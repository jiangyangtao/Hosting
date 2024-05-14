using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldItemAttribute : Attribute, IFieldGroup
    {
        public FieldItemAttribute(int sortIndex, string groupName = "")
        {
            SortIndex = sortIndex;
            GroupName = groupName;
        }

        public int SortIndex { set; get; }

        public string? GroupName { set; get; }
    }
}
