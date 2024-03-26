using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Yangtao.Hosting.NLog
{
    internal class LogTargetBuilder
    {
        public LogTargetBuilder()
        {
        }

        public static ColoredConsoleTarget BuildConsoleTarget()
        {
            var consoleTarget = new ColoredConsoleTarget
            {
                Layout = "${level}: ${longdate} ${newline}      ${callsite}    ${newline}      ${aspnet-request-ip:whenEmpty=localhost} ${newline}      ${message} ${exception:format=tostring}    ${newline}      ${aspnet-request-posted-body}",
            };
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

        public static AsyncTargetWrapper BuildFileTarget(FileLayoutType layoutType = FileLayoutType.Custom)
        {
            var fileTarget = new FileTarget
            {
                FileName = $"{LogPath}" + "${shortdate}.log",
                Layout = "LogTime: ${longdate} ${newline}      ErrorSource: ${logger} ${newline}    Message: ${message:raw=true} ${newline}    Exception: ${exception:format=tostring} ${newline}   RequestMethod: ${aspnet-request-method} ${newline}    RequestUrl: ${aspnet-request-url:IncludeHost=true:IncludePort=true:IncludeQueryString=true:IncludeScheme=true} ${newline}   InnerException:${event-properties:InnerException} ${newline}   StackTrace: ${event-properties:StackTrace} ${newline}",
            };

            if (layoutType == FileLayoutType.Json) fileTarget.Layout = LogLayoutBuilder.BuildJsonLayout();
            var fileTargetWrapper = new AsyncTargetWrapper
            {
                WrappedTarget = fileTarget,
            };

            return fileTargetWrapper;
        }

        private static string ApplicationName => Assembly.GetEntryAssembly().GetName().Name;

        private static bool IsLinuxOS => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        private static string LogPath
        {
            get
            {
                if (IsLinuxOS) return $"/var/log/application/{ApplicationName}/{LogLevel.Error}/";

                return $"{AppDomain.CurrentDomain.BaseDirectory}Log\\{LogLevel.Error}\\";
            }
        }
    }
}
