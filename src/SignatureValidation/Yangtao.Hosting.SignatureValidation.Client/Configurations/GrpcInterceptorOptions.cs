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
            var exist = ExistInterceptorMethod(method);
            if (exist) return this;

            InterceptorMethods.Add(method);
            return this;
        }

        internal bool ExistInterceptorMethod(string method) => InterceptorMethods.Any(a => a == method);
    }
}
