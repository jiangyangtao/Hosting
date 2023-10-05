namespace Yangtao.Hosting.SignatureValidation.Client.Abstractions
{
    public interface IClientEncryptionValidationProvider
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        string Encrypt(string plaintext);
    }
}
