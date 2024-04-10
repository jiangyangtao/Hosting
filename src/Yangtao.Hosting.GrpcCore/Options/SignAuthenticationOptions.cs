namespace Yangtao.Hosting.GrpcCore.Options
{
    public class SignAuthenticationOptions
    {
        public bool UseSignAuthenticationGrpcClientInterceptor { set; get; } = false;

        public SignAuthenticationType SignAuthenticationType { set; get; } = SignAuthenticationType.Aes;

        public AesSignOptions? AesSignOptions { set; get; }

        public RsaPrivateSignOptions? RsaPrivateSignOptions { set; get; }

        public RsaPublicSignOptions? RsaPublicSignOptions { set; get; }
    }
}
