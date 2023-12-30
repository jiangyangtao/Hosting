using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    public static class QueryableExtensions
    {
        public static IQueryable<TModel> Pagination<TModel>(this IQueryable<TModel> queryable, PaginationBase paginationParam) where TModel : IModel
        {
            return queryable.Skip(paginationParam.Start).Take(paginationParam.Size);
        }

        public static IQueryable<TEntity> OrderByDescCreateTime<TEntity>(this IQueryable<TEntity> queryable) where TEntity : IEntityBase
        {
            return queryable.OrderByDescending(a => a.CreateTime);
        }

        public static IQueryable<TEntity> DefaultOrderPagination<TEntity>(this IQueryable<TEntity> queryable, PaginationBase paginationParam) where TEntity : IEntityBase
        {
            return queryable.OrderByDescCreateTime().Pagination(paginationParam);
        }
    }
}
