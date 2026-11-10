using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Yangtao.Hosting.Repository.Abstractions;
using Yangtao.Hosting.Repository.Core.Repositories;

namespace Yangtao.Hosting.Repository.Core.Providers
{
    internal class EntityRepository<TEntity> : EntityRepositoryBase<TEntity, string>, IEntityRepository<TEntity> where TEntity : class, IEntity<string>, new()
    {
        public EntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(string id, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null) await DeleteAsync(entity, isCommit, cancellationToken);
        }

        public override async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public override async Task<TEntity> UpdateIfExistByIdAsync(string id, Action<TEntity> action, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            return await UpdateIfExistAsync(a => a.Id == id, action, isCommit, cancellationToken);
        }
    }

    internal class GuidEntityRepository<TEntity> : EntityRepositoryBase<TEntity, Guid>, IGuidEntityRepository<TEntity> where TEntity : class, IEntity<Guid>, new()
    {
        public GuidEntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(Guid id, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null) await DeleteAsync(entity, isCommit, cancellationToken);
        }

        public override async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public override async Task<TEntity> UpdateIfExistByIdAsync(Guid id, Action<TEntity> action, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            return await UpdateIfExistAsync(a => a.Id == id, action, isCommit, cancellationToken);
        }
    }

    internal class IntegerEntityRepository<TEntity> : EntityRepositoryBase<TEntity, int>, IIntegerEntityRepository<TEntity> where TEntity : class, IEntity<int>, new()
    {
        public IntegerEntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(int id, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null) await DeleteAsync(entity, isCommit, cancellationToken);
        }

        public override async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public override async Task<TEntity> UpdateIfExistByIdAsync(int id, Action<TEntity> action, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            return await UpdateIfExistAsync(a => a.Id == id, action, isCommit, cancellationToken);
        }
    }

    internal class BigIntegerEntityRepository<TEntity> : EntityRepositoryBase<TEntity, long>, IBigIntegerEntityRepository<TEntity> where TEntity : class, IEntity<long>, new()
    {
        public BigIntegerEntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override async Task DeleteByIdAsync(long id, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null) await DeleteAsync(entity, isCommit, cancellationToken);
        }

        public override Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default) => _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public override Task<TEntity> UpdateIfExistByIdAsync(long id, Action<TEntity> action, bool isCommit = true, CancellationToken cancellationToken = default)
        {
            return UpdateIfExistAsync(a => a.Id == id, action, isCommit, cancellationToken);
        }
    }
}