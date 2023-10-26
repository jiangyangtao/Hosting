using System.Linq.Expressions;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface IViewRepositoryProvider<TView> : ISqlQueryProvider<TView>, IEmptyArray<TView> where TView : BaseView
    {
        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<TView> Get();

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TView> Get(Expression<Func<TView, bool>> predicate);

        /// <summary>
        /// 获取一个不跟踪的 IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TView> GetNoTracking();

        /// <summary>
        /// 获取一个不跟踪的 IQueryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TView> GetNoTracking(Expression<Func<TView, bool>> predicate);
    }
}
