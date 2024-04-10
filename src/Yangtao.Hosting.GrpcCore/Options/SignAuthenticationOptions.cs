namespace Yangtao.Hosting.GrpcCore.Options
{
    public class SignAuthenticationOptions
    {
        internal SignAuthenticationOptions()
        {
        }

        public SignAuthenticationType? SignAuthenticationType { set; get; }

        public AesSignOptions? AesSignOptions { set; get; }

        public RsaPrivateSignOptions? RsaPrivateSignOptions { set; get; }

        public RsaPublicSignOptions? RsaPublicSignOptions { set; get; }
    }
}
