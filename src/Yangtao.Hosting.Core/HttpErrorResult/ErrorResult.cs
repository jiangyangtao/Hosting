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

        public static ErrorResult CreateErrorResult(string message)
        {
            return new ErrorResult
            {
                Code = -1,
                Message = message
            };
        }

        public static ErrorResult CreateErrorResult(int code, string message)
        {
            return new ErrorResult
            {
                Code = code,
                Message = message
            };
        }
    }
}
