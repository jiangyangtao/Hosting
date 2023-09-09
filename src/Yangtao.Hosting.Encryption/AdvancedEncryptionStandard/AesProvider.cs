using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Encryption.AdvancedEncryptionStandard
{
    public class AesProvider : AesBase
    {
        public AesProvider(string secretKey, string iv) : base(secretKey, iv)
        {
        }
    }
}
