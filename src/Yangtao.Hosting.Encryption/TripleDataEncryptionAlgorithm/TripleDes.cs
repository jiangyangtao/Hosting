
namespace Yangtao.Hosting.Encryption.TripleDataEncryptionAlgorithm
{
    public class TripleDes : TripleDesBase, IDisposable
    {
        public TripleDes(string secretKey, string iv) : base(secretKey, iv)
        {

        }

        public static TripleDes Create(string secretKey, string iv) => new(secretKey, iv);

        public void Dispose()
        {
            TripleDES.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
