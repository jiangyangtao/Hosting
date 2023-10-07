using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.SignatureValidation.Server.GrpcInterceptor
{
    internal class ServerSignatureValidationInterceptor : Interceptor
    {
        public ServerSignatureValidationInterceptor()
        {
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var httpContext = context.GetHttpContext();
            var endpoint = httpContext.GetEndpoint();


            return base.UnaryServerHandler(request, context, continuation);
        }
    }
}
