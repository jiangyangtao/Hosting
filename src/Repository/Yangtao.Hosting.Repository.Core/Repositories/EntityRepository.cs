using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;
using Yangtao.Hosting.Repository.Core.Repositories;

namespace Yangtao.Hosting.Repository.Core.Providers
{  
    internal class EntityRepository<TEntity> : EntityRepositoryBase<TEntity, string>, IEntityRepository<TEntity> where TEntity : class, IEntity<string>, new()
    {
        public EntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(string id, bool isCommit = true)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) await DeleteAsync(entity, isCommit);
        }

        public override async Task<TEntity?> GetByIdAsync(string id) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id);

        public override async Task<TEntity> UpdateIfExistByIdAsync(string id, Action<TEntity> action, bool isCommit = true)
        {
            return await UpdateIfExistAsync(a => a.Id == id, action, isCommit);
        }
    }

    internal class GuidEntityRepository<TEntity> : EntityRepositoryBase<TEntity, Guid>, IGuidEntityRepository<TEntity> where TEntity : class, IEntity<Guid>, new()
    {
        public GuidEntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(Guid id, bool isCommit = true)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) await DeleteAsync(entity, isCommit);
        }

        public override async Task<TEntity?> GetByIdAsync(Guid id) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id);

        public override async Task<TEntity> UpdateIfExistByIdAsync(Guid id, Action<TEntity> action, bool isCommit = true)
        {
            return await UpdateIfExistAsync(a => a.Id == id, action, isCommit);
        }
    }

    internal class IntegerEntityRepository<TEntity> : EntityRepositoryBase<TEntity, int>, IIntegerEntityRepository<TEntity> where TEntity : class, IEntity<int>, new()
    {
        public IntegerEntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(int id, bool isCommit = true)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) await DeleteAsync(entity, isCommit);
        }

        public override async Task<TEntity?> GetByIdAsync(int id) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id);

        public override async Task<TEntity> UpdateIfExistByIdAsync(int id, Action<TEntity> action, bool isCommit = true)
        {
            return await UpdateIfExistAsync(a => a.Id == id, action, isCommit);
        }
    }

    internal class BigIntegerEntityRepository<TEntity> : EntityRepositoryBase<TEntity, long>, IBigIntegerEntityRepository<TEntity> where TEntity : class, IEntity<long>, new()
    {
        public BigIntegerEntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(long id, bool isCommit = true)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) await DeleteAsync(entity, isCommit);
        }

        public override async Task<TEntity?> GetByIdAsync(long id) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id);

        public override async Task<TEntity> UpdateIfExistByIdAsync(long id, Action<TEntity> action, bool isCommit = true)
        {
            return await UpdateIfExistAsync(a => a.Id == id, action, isCommit);
        }
    }
}