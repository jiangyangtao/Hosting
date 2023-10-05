using RSAExtensions;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class RsaPublicProvider : IRsaPublicProvider
    {
        private readonly RSA Rsa;
        private readonly IClientConfigurationProvider _clientConfigurationProvider;

        public RsaPublicProvider(
                IClientConfigurationProvider clientConfigurationProvider)
        {
            _clientConfigurationProvider = clientConfigurationProvider;

            if (_clientConfigurationProvider.ClientValidationOptions.IsRsa == false) return;
            if (_clientConfigurationProvider.RsaPublicOptions == null) throw new NullReferenceException(nameof(RsaOptions));

            Rsa = RSA.Create();
            Rsa.ImportPublicKey(_clientConfigurationProvider.RsaPublicOptions.RSAKeyType, _clientConfigurationProvider.RsaPublicOptions.PublicKey);
        }

        public string Encrypt(string plaintext) => Rsa.EncryptBigData(plaintext, _clientConfigurationProvider.RsaPublicOptions.RSAEncryptionPadding);

        public bool VerifyData(string value, string signature)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));
            if (signature.IsNullOrEmpty()) throw new ArgumentNullException(nameof(signature));

            var signatureBytes = Convert.FromBase64String(signature);
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Rsa.VerifyData(valueBytes, signatureBytes, HashAlgorithmName.SHA256, _clientConfigurationProvider.RsaPublicOptions.RSAEncryptionPadding);
        }
    }
}
