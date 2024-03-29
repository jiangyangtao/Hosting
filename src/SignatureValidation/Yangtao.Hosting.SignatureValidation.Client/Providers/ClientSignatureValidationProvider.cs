﻿using Yangtao.Hosting.SignatureValidation.Client.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Client.Providers
{
    internal class ClientSignatureValidationProvider : IClientSignatureValidationProvider, IClientEncryptionValidationProvider
    {
        private readonly IClientHmacShaProvider _clientHmacShaProvider;
        private readonly IRsaPublicProvider _rsaPublicProvider;
        private readonly IClientConfigurationProvider _clientConfigurationProvider;

        public ClientSignatureValidationProvider(
            IClientHmacShaProvider clientHmacShaProvider,
            IRsaPublicProvider rsaPublicProvider,
            IClientConfigurationProvider clientConfigurationProvider)
        {
            _clientHmacShaProvider = clientHmacShaProvider;
            _rsaPublicProvider = rsaPublicProvider;
            _clientConfigurationProvider = clientConfigurationProvider;
        }

        public string Encrypt(string plaintext) => _rsaPublicProvider.Encrypt(plaintext);

        public string SignData(string value) => _clientHmacShaProvider.SignData(value);

        public bool VerifyData(string value, string signature)
        {
            if (_clientConfigurationProvider.ClientValidationOptions.IsHmacShaSignature)
                return _clientHmacShaProvider.VerifyData(value, signature);

            return _rsaPublicProvider.VerifyData(value, signature);
        }
    }
}
