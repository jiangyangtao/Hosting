using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Yangtao.Hosting.NLog.FileLog
{
    internal class FileLogBuilder
    {
        private readonly FileLogOptions _fileLogOptions;
        public const string TargetName = "LocalFileLog";

        public FileLogBuilder(FileLogOptions fileLogOptions)
        {
            _fileLogOptions = fileLogOptions;
        }

        public AsyncTargetWrapper FileTargetWrapper
        {
            get
            {
                var fileTarget = new FileTarget
                {
                    Name = TargetName,
                    FileName = _fileLogOptions.FileName,
                    Layout = _fileLogOptions.FileLayout,
                };

                var fileTargetWrapper = new AsyncTargetWrapper
                {
                    Name = TargetName,
                    WrappedTarget = fileTarget,
                };

                return fileTargetWrapper;
            }
        }

        public LoggingRule FileLogRule => _fileLogOptions.BuildLoggingRule(FileTargetWrapper);

        public LoggingConfiguration ConfigurationFileLog(LoggingConfiguration configuration)
        {
            configuration.AddTarget(FileTargetWrapper);
            configuration.LoggingRules.Add(FileLogRule);

            return configuration;
        }


        internal static Layout DefaultFileLogLayout => "LogTime: ${longdate} ${newline}${newline}        Level: ${level} ${newline}${newline}        Callsite: ${callsite} ${newline}${newline}        Stacktrace: ${stacktrace} ${newline}${newline}        ErrorSource: ${logger} ${newline}${newline}        Message: ${message:raw=true} ${newline}${newline}        Exception: ${exception:format=tostring} ${newline}${newline}${newline}${newline}${newline}${newline}${newline}${newline}";

        internal static string DefaultFileName => $"{LogPath}" + "${shortdate}.log";

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
