using System.Linq.Expressions;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface IEntityColumns<TEntity> : IEnumerable<Expression<Func<TEntity, object>>>, IDisposable where TEntity : IEntityBase
    {
        public void Add(Expression<Func<TEntity, object>> expression);

        public void Remove(Expression<Func<TEntity, object>> expression);

        public bool IsEmpty { get; }

        public string[] GetColumns();
    }
}
