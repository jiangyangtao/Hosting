namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class DataPagination<TData> where TData : class
    {
        public DataPagination()
        {
            List = Array.Empty<TData>();
        }

        public DataPagination(long count) : this()
        {
            Count = count;
        }

        public DataPagination(long count, IEnumerable<TData> list) : this(count)
        {
            List = list;
        }

        /// <summary>
        /// 数据总量
        /// </summary>
        public long Count { get; } = 0;

        public IEnumerable<TData> List { set; get; }
    }
}
