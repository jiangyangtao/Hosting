
using Yangtao.Hosting.Core;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class PaginationResult<TResult> where TResult : class
    {
        public PaginationResult()
        {
            List = Array.Empty<TResult>();
        }

        public PaginationResult(long count) : this()
        {
            Total = count;
        }

        public PaginationResult(long count, IEnumerable<TResult> list) : this(count)
        {
            List = list;
        }

        public PaginationResult(long count, Func<IEnumerable<TResult>> func) : this(count)
        {
            List = func();
        }

        /// <summary>
        /// 数据总量
        /// </summary>
        public long Total { get; } = 0;

        public IEnumerable<TResult> List { set; get; }
    }

    public class PaginationResult<TResult, TData> : PaginationResult<TResult> where TResult : class where TData : class
    {
        public PaginationResult() : base()
        {

        }

        public PaginationResult(DataPagination<TData> result, Func<TData, TResult> func) : base(result.Count)
        {
            if (result.List.NotNullAndEmpty()) List = result.List.Select(a => func(a));
        }
    }
}
