using Yangtao.Hosting.Common;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public abstract class QueryParamBase<TEntity> where TEntity : BaseEntity
    {
        public abstract IQueryable<TEntity> BuildQuery(IEntityRepository<TEntity> repository);
    }


    public abstract class PaginationQueryParamBase<TEntity> : PaginationBase<TEntity> where TEntity : BaseEntity
    {
        protected PaginationQueryParamBase(PagingParameter parameter)
        {
            Start = parameter.startIndex;
            Size = parameter.size;
            Sort = parameter.sort;
        }
    }
}
