﻿
namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class PaginationResult<T>
    {
        public PaginationResult(long count)
        {
            Total = count;
            List = Array.Empty<T>();
        }

        /// <summary>
        /// 数据总量
        /// </summary>
        public long Total { get; }

        public T[] List { set; get; }
    }
}
