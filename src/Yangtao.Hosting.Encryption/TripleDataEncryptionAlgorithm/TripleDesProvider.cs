using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Encryption.TripleDataEncryptionAlgorithm
{
    public class TripleDesProvider : TripleDesBase
    {
        public TripleDesProvider(string secretKey, string iv) : base(secretKey, iv)
        {
        }
    }
}
