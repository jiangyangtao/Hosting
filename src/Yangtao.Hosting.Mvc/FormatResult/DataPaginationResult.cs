namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class DataPaginationResult<TResult> : PaginationResult<TResult> where TResult : class
    {
        public DataPaginationResult(long count, Func<TResult[]> func) : base(count)
        {
            List = func();
        }
    }
}
