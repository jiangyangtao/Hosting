﻿using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client.Configurations
{
    public class RsaOptions : RsaBase
    {
        public string PublicKey { set; get; }
    }
}
