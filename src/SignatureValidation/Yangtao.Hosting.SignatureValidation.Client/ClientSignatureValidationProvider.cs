using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class ClientSignatureValidationProvider : IClientSignatureValidationProvider
    {
        public ClientSignatureValidationProvider()
        {
        }

        public string Encrypt(string plaintext)
        {
            throw new NotImplementedException();
        }

        public bool VerifyData(string value, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
