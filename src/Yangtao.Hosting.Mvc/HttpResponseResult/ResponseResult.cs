using Yangtao.Hosting.Mvc.Abstractions;

namespace Yangtao.Hosting.Mvc.HttpResponseResult
{
    internal class ResponseResult : IResponseResult
    {
        /// <summary>
        /// 初始化 <see cref="ResponseResult"/> 类的新实例
        /// </summary>
        private ResponseResult() { }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// 数据量
        /// </summary>
        public long Count { get; private set; }

        /// <summary>
        /// 错误信息 / 提示
        /// </summary>
        public string Message { get; private set; }

        public static IResponseResult CreateSuccessResult()
        {
            return new ResponseResult
            {
                Code = 0,
                Data = null,
                Message = string.Empty,
                Count = 0,
            };
        }

        public static IResponseResult CreateSuccessResult<TResult>(TResult result, long count = 0)
        {
            return new ResponseResult
            {
                Code = 0,
                Data = result,
                Message = string.Empty,
                Count = count,
            };
        }

        public static IResponseResult CreateSuccessResult<TResult>(IEnumerable<TResult> results, long count = 0)
        {
            return new ResponseResult
            {
                Code = 0,
                Data = results,
                Message = string.Empty,
                Count = count == 0 ? results.Count() : count,
            };
        }

        public static IResponseResult CreateFailResult(string message = "")
        {
            return new ResponseResult
            {
                Code = 0,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static IResponseResult CreateFailResult(int code, string message = "")
        {
            return new ResponseResult
            {
                Code = code,
                Data = null,
                Message = message,
                Count = 0,
            };
        }
    }
}
