
namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface IDataBaseRepository
    {
        IQueryable<TResult> SqlQuery<TResult>(FormattableString sql);

        IQueryable<TResult> SqlQueryRaw<TResult>(string sql, params object[] parameters);

        Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);

        Task<int> ExecuteSqlRawAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);



    }
}
