using System.Linq.Expressions;

namespace Yangtao.Hosting.SqlRepository.Abstractions
{
    public interface ISqlRepository
    {
        Task<int> InsertAsync<TEntity>(TEntity entity);

        Task<int> DeleteAsync<TEntity>(TEntity entity);

        Task<int> DeleteAsync<TEntity>(object Id);

        Task<int> DeleteRangeAsync<TEntity>(TEntity[] entities);

        Task<int> UpdateAsync<TEntity>(TEntity entity);

        Task<int> UpdatePartAsync<TEntity>(TEntity entity, Expression<Func<TEntity, object>> updateColumns);

        Task<int> UpdatePartAsync<TEntity>(TEntity entity, IEntityColumns<TEntity> updateColumns) where TEntity : class;

        Task<int> UpdatePropertyAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression);

        Task<int> UpdateRangePartAsync<TEntity>(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> updateColumns);

        Task<int> UpdateRangePartAsync<TEntity>((TEntity entities, IEntityColumns<TEntity> updateColumns)[] values) where TEntity : class;

        Task<int> UpdateRangePropertyAsync<TEntity, TProperty>(TEntity[] entities, Expression<Func<TEntity, TProperty>> propertyExpression);

        Task<int> UpdateIfExistByIdAsync<TEntity>(string id, Action<TEntity> action);

        Task<int> UpdateIfExistAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action);

        Task<int> UpdateRangeAsync<TEntity>(TEntity[] entities);

        Task<TEntity> GetByIdAsync<TEntity>(object id);

        Task<TEntity> GetAsync<TEntity>(string sql, params object[] objects);

        Task<TEntity[]> GetMultipleAsync<TEntity>(string sql, params object[] objects);

        Task<TProperty> GetPropertyAsync<TProperty>(string sql, params object[] objects);

        Task<long> GetCountAsync(string sql, params object[] objects);

        Task<TProperty[]> GetPropertiesAsync<TProperty>(string sql, params object[] objects);
    }
}
