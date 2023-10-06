namespace Yangtao.Hosting.SignatureValidation.Server.Abstractions
{
    public interface IServerEncryptionValidationProvider
    {
        string Decrypt(string ciphertext);
    }
}
