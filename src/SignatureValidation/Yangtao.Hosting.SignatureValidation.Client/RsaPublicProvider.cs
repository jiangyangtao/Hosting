using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class RsaPublicProvider : IRsaPublicProvider
    {
        private readonly RSA Rsa;
        private readonly RsaOptions rsaOptions;

        public RsaPublicProvider(IOptions<ClientValidationOptions> clientValidationOptions,
            )
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
