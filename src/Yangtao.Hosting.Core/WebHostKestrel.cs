using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace Yangtao.Hosting.Core
{
    public static class WebHostKestrel
    {
        public static void CustomConfigureKestrel(KestrelServerOptions serverOptions, IConfigurationSection configuration)
        {
            configuration.Get<KestrelConfigure>()?.ConfigureKestrel(serverOptions);
        }
    }
}
