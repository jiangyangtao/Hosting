using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcServer.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SignAuthentication : Attribute
    {
        public SignAuthentication(SignAuthenticationType authenticationType = SignAuthenticationType.Aes)
        {
            AuthenticationType = authenticationType;
        }

        public SignAuthenticationType AuthenticationType { get; }
    }
}
