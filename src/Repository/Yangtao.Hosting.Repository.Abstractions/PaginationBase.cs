using Microsoft.EntityFrameworkCore;

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

        public virtual Task<int> GetCountAsync(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.CountAsync();
        }

        public virtual IQueryable<TEntity> Pagination(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> OrderByDescCreateTime(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime);
        }

        public virtual IQueryable<TEntity> DefaultOrderPagination(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime).Skip(Start).Take(Size);
        }
    }
}
