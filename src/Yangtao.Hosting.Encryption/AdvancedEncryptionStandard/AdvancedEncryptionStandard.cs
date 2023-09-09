
namespace Yangtao.Hosting.Encryption.AdvancedEncryptionStandard
{
    public class AdvancedEncryptionStandard : AdvancedEncryptionStandardBase, IDisposable
    {
        public AdvancedEncryptionStandard(string secretKey, string iv) : base(secretKey, iv)
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
