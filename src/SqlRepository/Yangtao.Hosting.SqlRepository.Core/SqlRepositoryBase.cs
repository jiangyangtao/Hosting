using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using Yangtao.Hosting.SqlRepository.Abstractions;

namespace Yangtao.Hosting.SqlRepository.Core
{
    public abstract class SqlRepositoryBase : ISqlRepository
    {
        public SqlRepositoryBase()
        {

        }

        #region Transaction

        public IDbConnection BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbConnection BeginTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public Task<IDbConnection> BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDbConnection> BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Insert


        public virtual async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var connection = GetDbConnection();
            return await connection.InsertAsync(entity);
        }

        public Task<int> InsertMultipleAsync<TEntity>(TEntity[] entities) where TEntity : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete

        public virtual Task<int> DeleteAsync<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteAsync<TEntity>(long Id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteAsync<TEntity>(string Id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteRangeAsync<TEntity>(long[] ids)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteRangeAsync<TEntity>(string[] ids)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Update

        public virtual Task<int> UpdateAsync<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateIfExistAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateIfExistByIdAsync<TEntity>(string id, Action<TEntity> action)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdatePartAsync<TEntity>(TEntity entity, Expression<Func<TEntity, object>> updateColumns)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdatePartAsync<TEntity>(TEntity entity, IEntityColumns<TEntity> updateColumns) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdatePropertyAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateRangeAsync<TEntity>(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateRangePartAsync<TEntity>(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> updateColumns)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateRangePartAsync<TEntity>((TEntity entities, IEntityColumns<TEntity> updateColumns)[] values) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateRangePropertyAsync<TEntity, TProperty>(TEntity[] entities, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Query

        public virtual Task<TEntity> GetAsync<TEntity>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(long id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> GetCountAsync(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity[]> GetMultipleAsync<TEntity>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TProperty[]> GetPropertiesAsync<TProperty>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TProperty> GetPropertyAsync<TProperty>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetConnection
   

        protected abstract DbConnection GetDbConnection();

        protected abstract Task<DbConnection> GetDbConnectionAsync();

        #endregion
    }
}
