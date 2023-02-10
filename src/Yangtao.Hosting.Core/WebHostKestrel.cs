using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Authentication;

namespace Yangtao.Hosting.Core
{
    public static class WebHostKestrel
    {
        public static void CustomConfigureKestrel(KestrelServerOptions serverOptions, IConfigurationSection configuration)
        {
            var kestrelConfigure = configuration.Get<KestrelConfigure>();
            if (kestrelConfigure != null)
            {
                foreach (var endpoint in kestrelConfigure.Endpoints)
                {
                    Action<ListenOptions> action = (listenOptions) => { };
                    if (endpoint.Certificate != null)
                    {
                        action = (listenOptions) =>
                        {
                            var basePath = Directory.GetCurrentDirectory();
                            var certPath = Path.Combine(basePath!, endpoint.Certificate.Path, endpoint.Certificate.FileName);
                            listenOptions.UseHttps(certPath, endpoint.Certificate.Password, adapterOptions => adapterOptions.SslProtocols = SslProtocols.Tls13);
                        };
                    }

                    serverOptions.Listen(IPAddress.Loopback, endpoint.Port, action);
                }
            }
        }
    }
}
