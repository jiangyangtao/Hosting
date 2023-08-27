
namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface ISqlQueryProvider<out TModel> where TModel : IModel
    {
        IQueryable<TModel> SqlQuery(FormattableString sql);

        IQueryable<TModel> SqlQueryRaw(string sql, params object[] parameters);
    }
}
