
namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldGroupItemAttribute : Attribute
    {
        public FieldGroupItemAttribute(string groupName = "")
        {
            GroupName = groupName;
        }

        public string? GroupName { set; get; }
    }
}
