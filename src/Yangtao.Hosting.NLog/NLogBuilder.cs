using NLog;
using NLog.Layouts;
using NLog.Targets;

namespace Yangtao.Hosting.NLog
{
    internal class NLogBuilder
    {
        public NLogBuilder()
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

        public static ColoredConsoleTarget BuildConsoleTarget()
        {
            var consoleTarget = new ColoredConsoleTarget
            {
                Name = "CustomerColoredConsole",
                Layout = "${level}: ${longdate} ${newline}      ${callsite}    ${newline}      ${aspnet-request-ip:whenEmpty=localhost} ${newline}      ${message} ${exception:format=tostring}    ${newline}      ${aspnet-request-posted-body}",
                UseDefaultRowHighlightingRules = false,
            };
            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Trace",
                ForegroundColor = ConsoleOutputColor.Gray
            });
            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Fatal",
                ForegroundColor = ConsoleOutputColor.DarkRed
            });
            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Error",
                ForegroundColor = ConsoleOutputColor.Red
            });
            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Warning",
                ForegroundColor = ConsoleOutputColor.Yellow
            });
            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Info",
                ForegroundColor = ConsoleOutputColor.White
            });
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = LogLevel.Info.Name,
                ForegroundColor = ConsoleOutputColor.DarkGreen,
            });
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Condition = "level == LogLevel.Debug",
                Text = "[TEST]",
                ForegroundColor = ConsoleOutputColor.Blue,
            });

            return consoleTarget;
        }
    }
}
