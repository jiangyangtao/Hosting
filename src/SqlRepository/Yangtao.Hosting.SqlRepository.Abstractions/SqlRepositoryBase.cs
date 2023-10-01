using System.Data.Common;
using System.Linq.Expressions;

namespace Yangtao.Hosting.SqlRepository.Abstractions
{
    public abstract class SqlRepositoryBase : ISqlRepository
    {
        public SqlRepositoryBase()
        {

        }

        protected abstract DbConnection GetDbConnection();

        public Task<int> DeleteAsync<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(object Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRangeAsync<TEntity>(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync<TEntity>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync<TEntity>(object id)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity[]> GetMultipleAsync<TEntity>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public Task<TProperty[]> GetPropertiesAsync<TProperty>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public Task<TProperty> GetPropertyAsync<TProperty>(string sql, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateIfExistAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateIfExistByIdAsync<TEntity>(string id, Action<TEntity> action)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePartAsync<TEntity>(TEntity entity, Expression<Func<TEntity, object>> updateColumns)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePartAsync<TEntity>(TEntity entity, IEntityColumns<TEntity> updateColumns) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePropertyAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync<TEntity>(TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangePartAsync<TEntity>(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> updateColumns)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangePartAsync<TEntity>((TEntity entities, IEntityColumns<TEntity> updateColumns)[] values) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangePropertyAsync<TEntity, TProperty>(TEntity[] entities, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            throw new NotImplementedException();
        }
    }
}
