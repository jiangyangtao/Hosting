namespace Yangtao.Hosting.Mvc.HttpErrorResult
{
    public static class ResponseErrorResult
    {
        public static void ResponseBadRequest(string message) => throw new HttpErrorResult(400, message);

        public static void ResponseBadRequest(int code, string message) => throw new HttpErrorResult(400, code, message);



        public static void ResponseConflict(string message) => throw new HttpErrorResult(409, message);

        public static void ResponseConflict(int code, string message) => throw new HttpErrorResult(409, code, message);



        public static void ResponseForbid(string message) => throw new HttpErrorResult(403, message);

        public static void ResponseForbid(int code, string message) => throw new HttpErrorResult(403, code, message);



        public static void ResponseNotFound(string message) => throw new HttpErrorResult(404, message);

        public static void ResponseNotFound(int code, string message) => throw new HttpErrorResult(404, code, message);



        public static void ResponseUnauthorized(string message) => throw new HttpErrorResult(401, message);

        public static void ResponseUnauthorized(int code, string message) => throw new HttpErrorResult(401, code, message);



        public static void ResponseServerError(string message) => throw new HttpErrorResult(500, message);

        public static void ResponseServerError(int code, string message) => throw new HttpErrorResult(500, code, message);
    }
}
