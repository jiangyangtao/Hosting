﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core.Repositories
{
    internal abstract class EntityRepositoryBase<TEntity, TKeyType> : IEntityRepositoryBase<TEntity, TKeyType> where TEntity : class, IEntity<TKeyType>, new()
    {
        public const string UserIdPreperty = "UserId";

        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly DefaultDbContext _dbContext;

        protected readonly Type BaseModelType = typeof(TEntity);
        protected readonly Type TEntityType;
        protected readonly PropertyInfo[] EntityProperties;

        public EntityRepositoryBase(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;

            TEntityType = typeof(TEntity);
            EntityProperties = TEntityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        protected string UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor?.HttpContext?.User.Claims?.FirstOrDefault(a => a.Type == UserIdPreperty);
                return userIdClaim?.Value;
            }
        }

        public TEntity[] EmptyArray => Array.Empty<TEntity>();

        public async Task<TEntity> AddAsync(TEntity entity, bool isCommit = true)
        {
            if (entity == null) return null;

            entity.CreateUser = UserId;
            entity.UpdateUser = UserId;
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            await _dbContext.AddAsync(entity);

            if (isCommit) await CommitAsync();

            return entity;
        }

        public async Task<long> AddRangeAsync(TEntity[] entities, bool isCommit = true)
        {
            if (entities.IsNullOrEmpty()) return 0;

            foreach (var item in entities)
            {
                if (UserId.NotNullAndEmpty())
                {
                    item.CreateUser = UserId;
                    item.UpdateUser = UserId;
                }

                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false; // 批量操作需要关闭自动检测
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            if (isCommit) await CommitAsync();

            return entities.Count();
        }

        public async Task<long> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public abstract Task DeleteByIdAsync(TKeyType id, bool isCommit = true);


        public async Task<long> DeleteIfExistAsync(Expression<Func<TEntity, bool>> predicate, bool isCommit = true)
        {
            var entities = await Get(predicate).ToArrayAsync();
            if (entities.IsNullOrEmpty()) return 0;

            _dbContext.RemoveRange(entities);
            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public async Task<long> DeleteAsync(TEntity entity, bool isCommit = true)
        {
            if (entity == null) return 0;

            _dbContext.Remove(entity);
            if (isCommit) await CommitAsync();

            return 1;
        }

        public async Task<long> DeleteAllAsync(bool isCommit = true)
        {
            var list = _dbContext.Set<TEntity>().ToArray();
            _dbContext.RemoveRange(list);

            if (isCommit) await CommitAsync();

            return list.Length;
        }

        public async Task<long> DeleteRangeAsync(ICollection<TEntity> entities, bool isCommit = true)
        {
            if (entities.IsNullOrEmpty()) return 0;

            _dbContext.Set<TEntity>().RemoveRange(entities);
            if (isCommit) await CommitAsync();

            return entities.Count;
        }

        public IQueryable<TEntity> Get() => _dbContext.Set<TEntity>();

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => _dbContext.Set<TEntity>().Where(predicate);


        public IQueryable<TEntity> GetNoTracking() => _dbContext.Set<TEntity>().AsNoTracking();

        public IQueryable<TEntity> GetNoTracking(Expression<Func<TEntity, bool>> predicate) => _dbContext.Set<TEntity>().Where(predicate).AsNoTracking();

        public Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>> predicate) => _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();

        public abstract Task<TEntity?> GetByIdAsync(TKeyType id);

        public async Task<TEntity> UpdateAsync(TEntity entity, bool isCommit = true)
        {
            if (entity == null) return null;

            entity.UpdateTime = DateTime.Now;
            entity.UpdateUser = UserId;
            _dbContext.Entry(entity).State = EntityState.Modified;

            if (isCommit) await CommitAsync();

            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity, IEntityColumns<TEntity> updateColumns, bool isCommit = true) => UpdatePartAsync(entity, updateColumns, isCommit);

        public async Task<TEntity> UpdatePartAsync(TEntity entity, Expression<Func<TEntity, object>> updateColumns, bool isCommit = true)
        {
            if (entity == null) return null;
            if (updateColumns == null) return null;
            if (updateColumns.Body is not NewExpression newExpression) return null;
            if (newExpression.Members.IsNullOrEmpty()) return null;

            var columns = newExpression.Members.Select(a => a.Name).ToArray();
            if (columns.IsNullOrEmpty()) return null;

            foreach (var column in columns)
            {
                var isValidField = IsValidField(column);
                if (isValidField == false) continue;

                _dbContext.Entry(entity).Property(column).IsModified = true;
            }

            if (UserId.NotNullAndEmpty())
            {
                entity.UpdateUser = UserId;
                _dbContext.Entry(entity).Property(a => a.UpdateUser).IsModified = true;
            }

            entity.UpdateTime = DateTime.Now;
            _dbContext.Entry(entity).Property(a => a.UpdateTime).IsModified = true;

            if (isCommit) await CommitAsync();

            return entity;
        }

        public async Task<TEntity> UpdatePartAsync(TEntity entity, IEntityColumns<TEntity> updateColumns, bool isCommit = true)
        {
            if (entity == null) return null;
            if (updateColumns.IsNullOrEmpty()) return null;


            var columns = updateColumns.GetColumns();
            if (columns.IsNullOrEmpty()) return null;

            foreach (var column in columns)
            {
                var isValidField = IsValidField(column);
                if (isValidField == false) continue;

                _dbContext.Entry(entity).Property(column).IsModified = true;
            }

            if (UserId.NotNullAndEmpty())
            {
                entity.UpdateUser = UserId;
                _dbContext.Entry(entity).Property(a => a.UpdateUser).IsModified = true;
            }

            entity.UpdateTime = DateTime.Now;
            _dbContext.Entry(entity).Property(a => a.UpdateTime).IsModified = true;

            if (isCommit) await CommitAsync();

            return entity;
        }

        private static string AnalysisExpression(Expression expression)
        {
            if (expression is MemberExpression memberExpression) return memberExpression.Member.Name;
            if (expression is UnaryExpression unaryExpression) return AnalysisExpression(unaryExpression.Operand);

            return string.Empty;
        }

        public async Task<TEntity> UpdatePropertyAsync<TProperty>([NotNull] TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression, bool isCommit = true)
        {
            if (entity == null) return null;

            var name = propertyExpression.GetMemberAccess().Name;
            var isValidField = IsValidField(name);
            if (isValidField == false) return null;

            entity.UpdateTime = DateTime.Now;
            _dbContext.Entry(entity).Property(a => a.UpdateTime).IsModified = true;
            if (UserId.NotNullAndEmpty())
            {
                entity.UpdateUser = UserId;
                _dbContext.Entry(entity).Property(a => a.UpdateUser).IsModified = true;
            }

            _dbContext.Entry(entity).Property(propertyExpression).IsModified = true;

            if (isCommit) await CommitAsync();

            return entity;
        }

        public async Task<long> UpdateRangeAsync(TEntity[] entities, bool isCommit = true)
        {
            foreach (var entity in entities)
            {
                entity.UpdateTime = DateTime.Now;
                entity.UpdateUser = UserId;
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            _dbContext.UpdateRange(entities);
            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public async Task<long> UpdateRangePartAsync(TEntity[] entities, Expression<Func<TEntity, object>> updateColumns, bool isCommit = true)
        {
            if (entities.IsNullOrEmpty()) return 0;
            if (updateColumns == null) return 0;
            if (updateColumns.Body is not NewExpression newExpression) return 0;
            if (newExpression.Members.IsNullOrEmpty()) return 0;

            var columns = newExpression.Members.Select(a => a.Name).ToArray();
            if (columns.IsNullOrEmpty()) return 0;

            foreach (var entity in entities)
            {
                var hasModify = false;
                foreach (var column in columns)
                {
                    var isValidField = IsValidField(column);
                    if (isValidField == false) continue;

                    hasModify = true;
                    _dbContext.Entry(entity).Property(column).IsModified = true;
                }

                if (hasModify == false) continue;

                if (UserId.NotNullAndEmpty())
                {
                    entity.UpdateUser = UserId;
                    _dbContext.Entry(entity).Property(a => a.UpdateUser).IsModified = true;
                }

                entity.UpdateTime = DateTime.Now;
                _dbContext.Entry(entity).Property(a => a.UpdateTime).IsModified = true;
            }

            if (isCommit) await CommitAsync();

            return entities.Count();
        }

        public async Task<long> UpdateRangePartAsync((TEntity entities, IEntityColumns<TEntity> updateColumns)[] values, bool isCommit = true)
        {
            if (values.IsNullOrEmpty()) return 0;

            foreach (var (entity, updateColumns) in values)
            {
                if (entity == null) continue;

                var columns = updateColumns.GetColumns();
                if (columns.IsNullOrEmpty()) continue;

                foreach (var column in columns)
                {
                    var isValidField = IsValidField(column);
                    if (isValidField == false) continue;

                    _dbContext.Entry(entity).Property(column).IsModified = true;
                }

                if (UserId.NotNullAndEmpty())
                {
                    entity.UpdateUser = UserId;
                    _dbContext.Entry(entity).Property(a => a.UpdateUser).IsModified = true;
                }
            }

            if (isCommit) await CommitAsync();

            return values.Length;
        }

        public async Task<long> UpdateRangePropertyAsync<TProperty>(TEntity[] entities, Expression<Func<TEntity, TProperty>> propertyExpression, bool isCommit = true)
        {
            if (entities.IsNullOrEmpty()) return 0;

            var name = propertyExpression.GetMemberAccess().Name;
            var isValidField = IsValidField(name);
            if (isValidField == false) return 0;

            foreach (var entity in entities)
            {
                if (UserId.NotNullAndEmpty())
                {
                    entity.UpdateUser = UserId;
                    _dbContext.Entry(entity).Property(a => a.UpdateUser).IsModified = true;
                }

                entity.UpdateTime = DateTime.Now;
                _dbContext.Entry(entity).Property(a => a.UpdateTime).IsModified = true;
                _dbContext.Entry(entity).Property(propertyExpression).IsModified = true;
            }

            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public abstract Task<TEntity> UpdateIfExistByIdAsync(TKeyType id, Action<TEntity> action, bool isCommit = true);

        public async Task<TEntity> UpdateIfExistAsync(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action, bool isCommit = true)
        {
            var entity = await Get(predicate).FirstOrDefaultAsync();
            if (entity == null) return null;

            var cloneEntity = Clone(entity);
            action(cloneEntity);

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();
            var hasModify = false;
            var createTimeName = nameof(IEntity<TKeyType>.CreateTime);
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name == createTimeName) continue;

                var isValidField = IsValidField(propertyInfo.Name);
                if (isValidField == false) continue;

                var oldValue = propertyInfo.GetValue(entity);
                var newValue = propertyInfo.GetValue(cloneEntity);

                if (oldValue != newValue)
                {
                    propertyInfo.SetValue(entity, newValue);
                    _dbContext.Entry(entity).Property(propertyInfo.Name).IsModified = true;
                    hasModify = true;
                }
            }

            if (hasModify == false) return cloneEntity;

            if (UserId.NotNullAndEmpty())
            {
                entity.UpdateUser = UserId;
                _dbContext.Entry(entity).Property(a => a.UpdateUser).IsModified = true;
            }

            entity.UpdateTime = DateTime.Now;
            _dbContext.Entry(entity).Property(a => a.UpdateTime).IsModified = true;

            if (isCommit) await CommitAsync();

            return cloneEntity;
        }

        private bool IsValidField(string column)
        {
            var propertyInfo = EntityProperties.FirstOrDefault(a => a.Name == column);
            if (propertyInfo == null) return false;

            if (propertyInfo.CanWrite == false) return false;
            if (propertyInfo.Module != BaseModelType.Module) return false;

            var hasNotMappedAttribute = propertyInfo.GetCustomAttribute<NotMappedAttribute>();
            if (hasNotMappedAttribute != null) return false;

            var hasKeyAttribute = propertyInfo.GetCustomAttribute<KeyAttribute>();
            if (hasNotMappedAttribute != null) return false;

            return true;
        }

        private TEntity Clone(TEntity entity)
        {
            if (entity == null) return null;

            var type = entity.GetType();
            var newObject = Activator.CreateInstance(type);

            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite == false) continue;
                if (propertyInfo.Module != BaseModelType.Module) continue;

                var value = propertyInfo.GetValue(entity);
                propertyInfo.SetValue(newObject, value);
            }

            return (TEntity)newObject;
        }

        public IEntityColumns<TEntity> CreateEntityColumnExpressions() => new EntityColumns<TEntity>();

        public IQueryable<TEntity> SqlQuery(FormattableString sql) => _dbContext.Database.SqlQuery<TEntity>(sql);

        public IQueryable<TEntity> SqlQueryRaw(string sql, params object[] parameters) => _dbContext.Database.SqlQueryRaw<TEntity>(sql, parameters);
    }
}
