using Yangtao.Hosting.Mvc.Abstractions;

namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpSuccessResult : HttpResponseResult
    {
        public HttpSuccessResult(IResponseResult result) : base(200, result) { }

        public HttpSuccessResult() : this(ResponseResult.CreateSuccessResult()) { }
    }

    public class HttpSuccessResult<TResult> : HttpSuccessResult
    {
        public HttpSuccessResult(TResult result) : base(ResponseResult.CreateSuccessResult(result)) { }

        public HttpSuccessResult(TResult result, int count = 0) : base(ResponseResult.CreateSuccessResult(result, count)) { }

        public HttpSuccessResult(IEnumerable<TResult> results) : base(ResponseResult.CreateSuccessResult(results)) { }

        public HttpSuccessResult(IEnumerable<TResult> results, int count = 0) : base(ResponseResult.CreateSuccessResult(results, count)) { }
    }
}
