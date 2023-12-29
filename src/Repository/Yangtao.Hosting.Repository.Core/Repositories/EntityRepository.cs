using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    internal class EntityRepository<TEntity> : EntityRepositoryBase<TEntity, string>, IEntityRepository<TEntity> where TEntity : BaseEntity<string>, new()
    {
        public EntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {
        }

        public override Task<TEntity?> GetByIdAsync(string id) => _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id);


        public override Task<TEntity> UpdateIfExistByIdAsync(string id, Action<TEntity> action, bool isCommit = true) => UpdateIfExistAsync(a => a.Id == id, action, isCommit);
    }

    internal class EntityRepository<TEntity, TKeyType> : EntityRepositoryBase<TEntity, TKeyType>, IEntityRepository<TEntity, TKeyType> where TEntity : BaseEntity<TKeyType>, new() where TKeyType : struct
    {
        public EntityRepository(IHttpContextAccessor httpContextAccessor, DefaultDbContext dbContext) : base(httpContextAccessor, dbContext)
        {

        }

        public override Task<TEntity?> GetByIdAsync(TKeyType id) => _dbContext.Set<TEntity>().FirstOrDefaultAsync(a => a.Id.Equals(id));


        public override Task<TEntity> UpdateIfExistByIdAsync(TKeyType id, Action<TEntity> action, bool isCommit = true) => UpdateIfExistAsync(a => a.Id.Equals(id), action, isCommit);
    }
}