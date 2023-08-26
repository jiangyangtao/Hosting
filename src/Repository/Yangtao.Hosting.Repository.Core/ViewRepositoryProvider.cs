using System.Linq.Expressions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    internal class ViewRepositoryProvider<TView> : IViewRepositoryProvider<TView> where TView : BaseView
    {
        public ViewRepositoryProvider()
        {
        }

        public IQueryable<TView> Get()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TView> Get(Expression<Func<TView, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
