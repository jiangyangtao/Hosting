using Yangtao.Hosting.Abstractions.IResult;


namespace Yangtao.Hosting.Core.HttpErrorResult
{
    public class ErrorResult : IStatusResult
    {
        private ErrorResult()
        {
        }

        public int Code { get; private set; }

        public string Message { get; private set; }

        public static ErrorResult CreateErrorResult() => new()
        {
            Code = -1,
            Message = string.Empty,
        };

        public static ErrorResult CreateErrorResult(string message) => new()
        {
            Code = -1,
            Message = message
        };


        public static ErrorResult CreateErrorResult(int code, string message) => new()
        {
            Code = code,
            Message = message
        };
    }
}
