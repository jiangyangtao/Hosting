using Microsoft.Extensions.Options;
using Yangtao.Hosting.Encryption.AdvancedEncryptionStandard;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class AesSignProivder : ISignProvider
    {
        private readonly Aes? Aes;

        public AesSignProivder(IOptions<SignAuthenticationOptions> signOptions)
        {
            if (signOptions.Value.AesSignOptions != null)
            {
                Aes = new Aes(signOptions.Value.AesSignOptions.SecurityKey, signOptions.Value.AesSignOptions.Iv);
            }
        }

        public SignAuthenticationType SignAuthenticationType => SignAuthenticationType.Aes;

        public string Decrypt(string value)
        {
            if (Aes == null) throw new MemberAccessException(nameof(AesSignOptions));

            return Aes.Decrypt(value);
        }

        public string Encrypt(string value)
        {
            if (Aes == null) throw new MemberAccessException(nameof(AesSignOptions));

            return Aes.Encrypt(value);
        }
    }
}
