using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using System.Security.Authentication;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Core
{
    public class KestrelConfigure
    {
        public Endpoint[] Endpoints { set; get; }

        public void ConfigureKestrel(KestrelServerOptions serverOptions)
        {
            foreach (var endpoint in Endpoints)
            {
                serverOptions.Listen(endpoint.Address, endpoint.Port, endpoint.ConfigListenOptions);
            }
        }
    }

    public class Endpoint
    {
        public int Port { set; get; }

        public string IPAddress { set; get; }

        public IPAddress Address
        {
            get
            {
                if (IPAddress.IsNullOrEmpty()) return null;
                if (IPAddress.Equals("Any", StringComparison.OrdinalIgnoreCase)) return System.Net.IPAddress.Any;
                if (IPAddress.Equals("Loopback", StringComparison.OrdinalIgnoreCase)) return System.Net.IPAddress.Loopback;

                return System.Net.IPAddress.Parse(IPAddress);
            }
        }

        public Certificate Certificate { set; get; }

        public void ConfigListenOptions(ListenOptions listenOptions) => Certificate?.ConfigHttps(listenOptions);
    }

    public class Certificate
    {
        public string Path { set; get; }

        public string FileName { set; get; }

        public string Password { set; get; }

        public SslProtocols SslProtocols { set; get; }

        public string CertPath
        {
            get
            {
                var basePath = Directory.GetCurrentDirectory();
                return System.IO.Path.Combine(basePath!, Path, FileName);
            }
        }

        public void ConfigHttps(ListenOptions listenOptions)
        {
            listenOptions.UseHttps(CertPath, Password, adapterOptions => adapterOptions.SslProtocols = SslProtocols);
        }
    }
}
