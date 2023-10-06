namespace Yangtao.Hosting.SignatureValidation.Client.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ClientSignatureValidationAttribute : Attribute { }
}
