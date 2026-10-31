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
    }
}
