using RSAExtensions;
using System.Security.Cryptography;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;

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
            throw new NotImplementedException();
        }
    }
}
