using Yangtao.Hosting.Repository.Abstractions;

namespace Test.Repository.Application.Models
{
    public class Product : BaseEntity
    {
        public string? ProductCode { set; get; }

        public string? ProductName { set; get; }
    }

    public class ProductQueryParam : PaginationBase<Product>
    {

        public override IQueryable<Product> BuildQuery(IEntityRepository<Product> repository)
        {
            throw new NotImplementedException();
        }
    }
}
