using System.Data;
using System.Linq.Expressions;

namespace Yangtao.Hosting.SqlRepository.Abstractions
{
    public interface ISqlRepository
    {
        #region Transaction

        IDbConnection BeginTransaction();

        IDbConnection BeginTransaction(IsolationLevel isolationLevel);

        Task<IDbConnection> BeginTransactionAsync();

        Task<IDbConnection> BeginTransactionAsync(IsolationLevel isolationLevel);

        #endregion

        #region Insert

        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;

        Task<int> InsertMultipleAsync<TEntity>(TEntity[] entities) where TEntity : class;

        #endregion

        #region Delete

        Task<int> DeleteAsync<TEntity>(TEntity entity);

        Task<int> DeleteAsync<TEntity>(long Id);

        Task<int> DeleteAsync<TEntity>(string Id);

        Task<int> DeleteRangeAsync<TEntity>(long[] ids);

        Task<int> DeleteRangeAsync<TEntity>(string[] ids);

        #endregion

        #region Update


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

        #endregion

        #region Query

        Task<TEntity> GetByIdAsync<TEntity>(string id);

        Task<TEntity> GetByIdAsync<TEntity>(long id);

        Task<TEntity> GetAsync<TEntity>(string sql, params object[] objects);

        Task<TEntity[]> GetMultipleAsync<TEntity>(string sql, params object[] objects);

        Task<TProperty> GetPropertyAsync<TProperty>(string sql, params object[] objects);

        Task<long> GetCountAsync(string sql, params object[] objects);

        Task<TProperty[]> GetPropertiesAsync<TProperty>(string sql, params object[] objects);

        #endregion
    }
}
