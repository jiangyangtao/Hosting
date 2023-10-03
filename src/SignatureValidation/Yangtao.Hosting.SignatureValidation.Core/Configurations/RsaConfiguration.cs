using System.Text.Json.Serialization;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaConfigurationBase
    {
        public RsaAlgorithmType AlgorithmType { set; get; }

        public RSAKeyType RSAKeyType { set; get; }

        public string Algorithm
        {
            get
            {
                if (AlgorithmType == RsaAlgorithmType.RsaSha256) return SecurityAlgorithms.RsaSha256;
                if (AlgorithmType == RsaAlgorithmType.RsaSha384) return SecurityAlgorithms.RsaSha384;

                return SecurityAlgorithms.RsaSha512;
            }
        }
    }
}
