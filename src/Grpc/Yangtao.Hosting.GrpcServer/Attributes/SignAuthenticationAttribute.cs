namespace Yangtao.Hosting.GrpcServer.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SignAuthenticationAttribute : Attribute
    {
        public SignAuthenticationAttribute()
        {
        }

    }
}
