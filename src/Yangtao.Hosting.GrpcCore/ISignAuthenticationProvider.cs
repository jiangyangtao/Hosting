namespace Yangtao.Hosting.GrpcCore
{
    public interface ISignAuthenticationProvider
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}
