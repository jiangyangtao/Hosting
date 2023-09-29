using Org.BouncyCastle.Asn1.Ocsp;
using RSAExtensions;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    internal class RsaPrivateProvider : IRsaPrivateProvider
    {
        private readonly RSA Rsa;
        private readonly IServerConfigurationProvider _serverConfigurationProvider;

        public RsaPrivateProvider(IServerConfigurationProvider serverConfigurationProvider)
        {
            _serverConfigurationProvider = serverConfigurationProvider;

            if (_serverConfigurationProvider.ServerValidationOptions.IsRsaEncryption)
            {
                if (_serverConfigurationProvider.RsaEncryptionOptions == null) throw new NullReferenceException(nameof(RsaEncryptionOptions));

                Rsa = RSA.Create();
                Rsa.ImportPublicKey(_serverConfigurationProvider.RsaEncryptionOptions.RSAKeyType, _serverConfigurationProvider.RsaEncryptionOptions.PrivateKey);
            }

            if (_serverConfigurationProvider.ServerValidationOptions.IsRsaSignature)
            {
                if (_serverConfigurationProvider.RsaSignatureOptions == null) throw new NullReferenceException(nameof(RsaSignatureOptions));

                Rsa = RSA.Create();
                Rsa.ImportPublicKey(_serverConfigurationProvider.RsaSignatureOptions.RSAKeyType, _serverConfigurationProvider.RsaSignatureOptions.PrivateKey);
            }
        }

        public string Decrypt(string ciphertext) => Rsa.DecryptBigData(ciphertext, _serverConfigurationProvider.RsaEncryptionOptions.RSAEncryptionPadding);

        public string SignData(string value)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));

            var valueBytes = Encoding.UTF8.GetBytes(value);
            var resultBytes = Rsa.SignData(valueBytes, _serverConfigurationProvider.RsaSignatureOptions.HashAlgorithmName, _serverConfigurationProvider.RsaSignatureOptions.RSASignaturePadding);
            return Convert.ToBase64String(resultBytes);
        }
    }
}
