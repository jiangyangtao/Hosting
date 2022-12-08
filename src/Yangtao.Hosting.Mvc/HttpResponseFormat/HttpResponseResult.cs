

namespace Yangtao.Hosting.Mvc.HttpResponseFormat
{
    public class HttpResponseResult
    {
        public HttpResponseResult() { }

        public static ResponseResult ResponseSuccess()
        {
            return new ResponseResult
            {
                Code = 0,
                Data = null,
                Message = string.Empty,
                Count = 0,
            };
        }

        public static ResponseResult ResponseSuccess<TData>(TData data, long count = 0)
        {
            return new ResponseResult
            {
                Code = 0,
                Data = data,
                Message = string.Empty,
                Count = count,
            };
        }

        public static ResponseResult ResponseSuccess<TData>(IEnumerable<TData> data, long count = 0)
        {
            return new ResponseResult
            {
                Code = 0,
                Data = data,
                Message = string.Empty,
                Count = count == 0 ? data.Count() : count,
            };
        }

        public static ResponseResult ResponseFail(string message = "")
        {
            return new ResponseResult
            {
                Code = 0,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseFail(int code, string message = "")
        {
            return new ResponseResult
            {
                Code = code,
                Data = null,
                Message = message,
                Count = 0,
            };
        }





        public static ResponseResult ResponseBadRequest(string message = "")
        {
            return new ResponseResult
            {
                Code = 0,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseBadRequest(int code, string message = "")
        {
            return new ResponseResult
            {
                Code = code,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseNotFound(string message = "")
        {
            return new ResponseResult
            {
                Code = -1,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseNotFound(int code, string message = "")
        {
            return new ResponseResult
            {
                Code = code,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseServerError(string message = "")
        {
            return new ResponseResult
            {
                Code = -1,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseServerError(int code, string message = "")
        {
            return new ResponseResult
            {
                Code = code,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseConflict(string message = "")
        {
            return new ResponseResult
            {
                Code = -1,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseConflict(int code, string message = "")
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
