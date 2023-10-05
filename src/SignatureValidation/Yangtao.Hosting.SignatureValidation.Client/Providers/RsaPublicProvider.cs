using RSAExtensions;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client.Providers
{
    internal class RsaPublicProvider : IRsaPublicProvider
    {
        private readonly RSA Rsa;
        private readonly IClientConfigurationProvider _clientConfigurationProvider;

        public RsaPublicProvider(IClientConfigurationProvider clientConfigurationProvider)
        {
            _clientConfigurationProvider = clientConfigurationProvider;

            if (_clientConfigurationProvider.ClientValidationOptions.IsRsaEncryption)
            {
                if (_clientConfigurationProvider.RsaEncryptionOptions == null) throw new NullReferenceException(nameof(RsaEncryptionOptions));

                Rsa = RSA.Create();
                Rsa.ImportPublicKey(_clientConfigurationProvider.RsaEncryptionOptions.RSAKeyType, _clientConfigurationProvider.RsaEncryptionOptions.PublicKey);
            }

            if (_clientConfigurationProvider.ClientValidationOptions.IsRsaSignature)
            {
                if (_clientConfigurationProvider.RsaSignatureOptions == null) throw new NullReferenceException(nameof(RsaSignatureOptions));

                Rsa = RSA.Create();
                Rsa.ImportPublicKey(_clientConfigurationProvider.RsaSignatureOptions.RSAKeyType, _clientConfigurationProvider.RsaSignatureOptions.PublicKey);
            }

        }

        public string Encrypt(string plaintext) => Rsa.EncryptBigData(plaintext, _clientConfigurationProvider.RsaEncryptionOptions.RSAEncryptionPadding);

        public bool VerifyData(string value, string signature)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));
            if (signature.IsNullOrEmpty()) throw new ArgumentNullException(nameof(signature));

            var signatureBytes = Convert.FromBase64String(signature);
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Rsa.VerifyData(valueBytes, signatureBytes, _clientConfigurationProvider.RsaSignatureOptions.HashAlgorithmName, _clientConfigurationProvider.RsaSignatureOptions.RSASignaturePadding);
        }
    }
}
