using Grpc.Core;
using Grpc.Core.Interceptors;
using Yangtao.Hosting.Core.HttpErrorResult;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.GrpcServer.Interceptors
{
    /// <summary>
    /// GRPC 全局侦听器
    /// </summary>
    internal class GrpcGlobalInterceptor : Interceptor
    {
        public GrpcGlobalInterceptor()
        {
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception e)
            {
                var (code, message) = GetErrorResult(e);
                var response = CreateErrorResponse(continuation, code, message);
                if (response == null) throw new RpcException(Status.DefaultCancelled, e.Message);

                return (TResponse)response;
            }

        }

        private static (int code, string message) GetErrorResult(Exception exception)
        {
            var code = -1;
            var message = exception.GetInnerException().Message;
            if (exception is HttpErrorResult httpError)
            {
                var result = httpError.GetErrorResult();
                code = result.Code;
                message = result.Message;
            }

            return (code, message);
        }

        private static object CreateErrorResponse<TRequest, TResponse>(UnaryServerMethod<TRequest, TResponse> continuation, int code, string message) where TRequest : class where TResponse : class
        {
            var responseType = continuation.Target.GetType().GenericTypeArguments.LastOrDefault();
            var response = Activator.CreateInstance(responseType);
            var propertyInfos = responseType.GetProperties();
            var errorResultType = propertyInfos.FirstOrDefault(a => a.Name.Contains("Error"));
            if (errorResultType == null) return null;

            var errorResult = Activator.CreateInstance(errorResultType.PropertyType);
            var errorPropertyInfos = errorResultType.PropertyType.GetProperties();
            foreach (var item in errorPropertyInfos)
            {
                if (item.Name == "Code") item.SetValue(errorResult, code, null);
                if (item.Name == "Message") item.SetValue(errorResult, message, null);
            }
            errorResultType.SetValue(response, errorResult, null);

            return response;
        }
    }
}
