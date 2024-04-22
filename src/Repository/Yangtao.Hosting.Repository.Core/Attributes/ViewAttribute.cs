using System.ComponentModel.DataAnnotations.Schema;

namespace Yangtao.Hosting.Repository.Core.Attributes
{
    internal class ViewAttribute : TableAttribute
    {
        public ViewAttribute(string name) : base(name)
        {
        }
    }
}
