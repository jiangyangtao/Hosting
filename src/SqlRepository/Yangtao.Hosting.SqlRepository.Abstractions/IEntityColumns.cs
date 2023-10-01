using System.Linq.Expressions;

namespace Yangtao.Hosting.SqlRepository.Abstractions
{
    public interface IEntityColumns<TEntity> : IEnumerable<Expression<Func<TEntity, object>>>, IDisposable where TEntity : class
    {
        public void Add(Expression<Func<TEntity, object>> expression);

        public void Remove(Expression<Func<TEntity, object>> expression);

        public bool IsEmpty { get; }

        public string[] GetColumns();

        public string GetUpdateSetSql(TEntity entity);
    }
}
