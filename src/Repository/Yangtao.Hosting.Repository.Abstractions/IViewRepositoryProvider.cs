using System.Linq.Expressions;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface IViewRepositoryProvider<TView> where TView : BaseView
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
    }
}
