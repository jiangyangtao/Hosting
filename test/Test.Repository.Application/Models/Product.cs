using Yangtao.Hosting.Repository.Abstractions;

namespace Test.Repository.Application.Models
{
    public class Product : BaseEntity
    {
        public string? ProductCode { set; get; }

        public string? ProductName { set; get; }
    }
}
