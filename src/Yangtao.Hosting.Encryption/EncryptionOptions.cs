
namespace Yangtao.Hosting.Encryption
{
    public class EncryptionOptions
    {
        internal EncryptionOptions()
        {
        }

        public string SecretKey { get; }

        public string Iv { get; }
    }
}
