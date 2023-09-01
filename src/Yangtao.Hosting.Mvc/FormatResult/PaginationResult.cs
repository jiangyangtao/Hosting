
namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class PaginationResult<T>
    {
        public PaginationResult(long count)
        {
            TotalCount = count;
            List = Array.Empty<T>();
        }

        /// <summary>
        /// 数据总量
        /// </summary>
        public long TotalCount { get; }

        public T[] List { set; get; }
    }
}
