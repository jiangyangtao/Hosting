﻿using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.Common
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PagingParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public PagingParameter()
        {

        }

        /// <summary>
        /// 列表起始位置，int，默认为 0，取值范围[0,2147483647]
        /// </summary>
        /// <example>0</example>
        [Range(0, int.MaxValue, ErrorMessage = "startIndex 的值范围 0 - 2147483647")]
        public int startIndex { get; set; } = 0;

        /// <summary>
        /// 获取列表条数，int，默认为 10，取值范围[1,200]
        /// </summary>
        /// <example>10</example>
        [Range(1, 200, ErrorMessage = "size 的值范围 1 - 200")]
        public int size { get; set; } = 10;
    }
}
