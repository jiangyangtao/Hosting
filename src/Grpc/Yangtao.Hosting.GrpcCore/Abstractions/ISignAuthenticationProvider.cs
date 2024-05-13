namespace Yangtao.Hosting.GrpcCore.Abstractions
{
    public interface ISignAuthenticationProvider
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}
