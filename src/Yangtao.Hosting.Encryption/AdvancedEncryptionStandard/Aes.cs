
namespace Yangtao.Hosting.Encryption.AdvancedEncryptionStandard
{
    public class Aes : AesBase, IDisposable
    {
        public Aes(string secretKey, string iv) : base(secretKey, iv)
        {
        }

        public static Aes Create(string secretKey, string iv) => new(secretKey, iv);

        public void Dispose()
        {
            Aes.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
