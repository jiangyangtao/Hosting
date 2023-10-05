namespace Yangtao.Hosting.SignatureValidation.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ServerSignatureValidationAttribute : Attribute    {  }
}
