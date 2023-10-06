namespace Yangtao.Hosting.SignatureValidation.Client.Configurations
{
    public class GrpcInterceptorOptions
    {
        internal IList<string> InterceptorMethods;

        public GrpcInterceptorOptions()
        {
            InterceptorMethods = new List<string>();
        }

        public GrpcInterceptorOptions AddInterceptorMethod(string method)
        {
            if (InterceptorMethods.Any(a => a == method)) return this;

            InterceptorMethods.Add(method);
            return this;
        }
    }
}
