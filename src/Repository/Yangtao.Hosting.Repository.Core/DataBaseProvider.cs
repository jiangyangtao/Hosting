using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    internal class DataBaseProvider : IDataBaseProvider
    {
        private readonly DbContext _dbContext;

        public DataBaseProvider(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TResult> SqlQuery<TResult>(FormattableString sql) => _dbContext.Database.SqlQuery<TResult>(sql);

        public IQueryable<TResult> SqlQueryRaw<TResult>(string sql, params object[] parameters) => _dbContext.Database.SqlQueryRaw<TResult>(sql);

        public Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default) =>
           _dbContext.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);

        public Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default) => _dbContext.Database.ExecuteSqlAsync(sql, cancellationToken);

        public Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default) => _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        public Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters) => _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);

        public Task<int> ExecuteSqlRawAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default) =>
             _dbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);


        public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();


        public IDbContextTransaction BeginTransaction(IsolationLevel level) => _dbContext.Database.BeginTransaction(level);


        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => _dbContext.Database.BeginTransactionAsync(cancellationToken);


        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default) =>
                _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);


        public IDbContextTransaction? UseTransaction(DbTransaction? transaction) => _dbContext.Database.UseTransaction(transaction);


        public IDbContextTransaction? UseTransaction(DbTransaction? transaction, Guid transactionId) => _dbContext.Database.UseTransaction(transaction, transactionId);


        public Task<IDbContextTransaction?> UseTransactionAsync(DbTransaction? transaction, CancellationToken cancellationToken = default) =>
            _dbContext.Database.UseTransactionAsync(transaction, cancellationToken);


        public Task<IDbContextTransaction?> UseTransactionAsync(DbTransaction? transaction, Guid transactionId, CancellationToken cancellationToken = default) =>
            _dbContext.Database.UseTransactionAsync(transaction, transactionId, cancellationToken);

        public void CommitTransaction() => _dbContext.Database.CommitTransaction();

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default) => _dbContext.Database.CommitTransactionAsync(cancellationToken);

        public void RollbackTransaction() => _dbContext.Database.RollbackTransaction();

        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }
}
