
namespace Yangtao.Hosting.Repository.Abstractions
{
    public abstract class PaginationBase
    {
        public int Start { set; get; } = 0;

        public int Size { set; get; } = 10;
    }

    public abstract class PaginationBase<TEntity> : PaginationBase where TEntity : BaseEntity
    {
        public abstract IQueryable<TEntity> BuildQuery(IEntityRepository<TEntity> repository);
    }
}
