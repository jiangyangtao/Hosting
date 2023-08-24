using NLog.Layouts;

namespace Yangtao.Hosting.NLog
{
    internal class LogLayoutBuilder
    {
        public LogLayoutBuilder()
        {
        }

        public static JsonLayout BuildJsonLayout()
        {
            var jsonLayout = new JsonLayout();
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "LogTime",
                Layout = "${longdate}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "Level",
                Layout = "${level}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "ErrorSource",
                Layout = "${logger}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "Message",
                Layout = "${message:raw=true}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "Exception",
                Layout = "${exception:format=tostring}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "ClientIP",
                Layout = "${aspnet-request-ip:whenEmpty=localhost}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "RequestMethod",
                Layout = "${aspnet-request-method}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "RequestUrl",
                Layout = "${aspnet-request-url:IncludeHost=true:IncludePort=true:IncludeQueryString=true:IncludeScheme=true}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "RequestHeaders",
                Layout = "${aspnet-request-headers:itemSeparator=,}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "RequestBody",
                Layout = "${aspnet-request-posted-body}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "QueryString",
                Layout = "${aspnet-request-querystring:itemSeparator=&}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "UserAgent",
                Layout = "${aspnet-request-useragent}"
            });
            jsonLayout.Attributes.Add(new JsonAttribute
            {
                Name = "Properties",
                Encode = false,
                Layout = new JsonLayout
                {
                    IncludeEventProperties = true,
                }
            });

            return jsonLayout;
        }
    }
}
