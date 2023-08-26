using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    internal class ViewRepositoryProvider<TView> : IViewRepositoryProvider<TView> where TView : BaseView
    {
        private readonly DbContext _dbContext;

        public ViewRepositoryProvider(DefaultDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TView> Get()
        {
            return _dbContext.Set<TView>();
        }

        public IQueryable<TView> Get(Expression<Func<TView, bool>> predicate)
        {
            return _dbContext.Set<TView>().Where(predicate);
        }
    }
}
