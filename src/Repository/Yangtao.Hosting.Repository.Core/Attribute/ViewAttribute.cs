using System.ComponentModel.DataAnnotations.Schema;

namespace Yangtao.Hosting.Repository.Core.Attribute
{
    internal class ViewAttribute : TableAttribute
    {
        public ViewAttribute(string name) : base(name)
        {
        }
    }
}
