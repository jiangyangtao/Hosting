using Microsoft.Extensions.Options;
using Yangtao.Hosting.Encryption.RsaAlgorithm;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class RsaSignProvider : ISignProvider
    {
        private readonly RsaPrivate? RsaPrivate;
        private readonly RsaPublic? RsaPublic;

        public RsaSignProvider(IOptions<SignAuthenticationOptions> signOptions)
        {
            var options = signOptions.Value;
            if (options.RsaPrivateSignOptions != null) RsaPrivate = new RsaPrivate(options.RsaPrivateSignOptions.PrivateKey, options.RsaPrivateSignOptions.RSAKeyType);
            if (options.RsaPublicSignOptions != null) RsaPublic = new RsaPublic(options.RsaPublicSignOptions.PublicKey, options.RsaPublicSignOptions.RSAKeyType);
        }

        public SignAuthenticationType SignAuthenticationType => SignAuthenticationType.RSA;

        public string Decrypt(string value)
        {
            if (RsaPrivate == null) throw new MemberAccessException(nameof(RsaPrivateSignOptions));

            return RsaPrivate.Decrypt(value);
        }

        public string Encrypt(string value)
        {
            if (RsaPublic == null) throw new MemberAccessException(nameof(RsaPublicSignOptions));

            return RsaPublic.Encrypt(value);
        }
    }
}
