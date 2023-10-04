using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal class HmacShaPorviderFactory : IHmacShaPorviderFactory
    {
        public HmacShaPorviderFactory()
        {
        }

        public IHmacShaProvider CreateIHmacShaProvider()
        {
            throw new NotImplementedException();
        }
    }
}
