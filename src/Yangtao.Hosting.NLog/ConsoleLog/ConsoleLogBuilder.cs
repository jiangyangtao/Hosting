using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.NLog.ConsoleLog
{
    internal class ConsoleLogBuilder
    {
        private readonly ConsoleLogOptions _consoleLogOptions;

        private static Layout DefaultConsoleLayout => "${level}: ${longdate} ${newline}      ${callsite}    ${newline}      ${aspnet-request-ip:whenEmpty=localhost} ${newline}      ${message} ${exception:format=tostring}    ${newline}      ${aspnet-request-posted-body}";

        public ConsoleLogBuilder(ConsoleLogOptions consoleLogOptions)
        {
            _consoleLogOptions = consoleLogOptions;
        }

        public ColoredConsoleTarget ConsoleTarget
        {
            get
            {
                var layout = _consoleLogOptions.Layout ?? DefaultConsoleLayout;
                var consoleTarget = new ColoredConsoleTarget
                {
                    Name = "CustomColoredConsole",
                    Layout = layout,
                    UseDefaultRowHighlightingRules = _consoleLogOptions.UseDefaultRowHighlightingRules,
                };
                if (_consoleLogOptions.UseDefaultRowHighlightingRules == false)
                {
                    if (_consoleLogOptions.RowHighlightingRules.IsNullOrEmpty())
                    {
                        foreach (var rowHighlightingRule in DefaultRowHighlightingRules)
                        {
                            consoleTarget.RowHighlightingRules.Add(rowHighlightingRule);
                        }
                    }

                    if (_consoleLogOptions.RowHighlightingRules.NotNullAndEmpty())
                    {
                        foreach (var rowHighlightingRule in _consoleLogOptions.RowHighlightingRules)
                        {
                            consoleTarget.RowHighlightingRules.Add(rowHighlightingRule);
                        }
                    }

                    if (_consoleLogOptions.WordHighlightingRules.IsNullOrEmpty())
                    {
                        foreach (var wordHighlightingRule in DefaultWordHighlightingRules)
                        {
                            consoleTarget.WordHighlightingRules.Add(wordHighlightingRule);
                        }
                    }

                    if (_consoleLogOptions.WordHighlightingRules.NotNullAndEmpty())
                    {
                        foreach (var wordHighlightingRule in _consoleLogOptions.WordHighlightingRules)
                        {
                            consoleTarget.WordHighlightingRules.Add(wordHighlightingRule);
                        }
                    }
                }

                return consoleTarget;
            }
        }

        public LoggingRule ConsoleLogRule => _consoleLogOptions.BuildLoggingRule(ConsoleTarget);

        public LoggingConfiguration ConfigurationFileLog(LoggingConfiguration configuration)
        {
            configuration.AddTarget(ConsoleTarget);
            configuration.LoggingRules.Add(ConsoleLogRule);

            return configuration;
        }

        private static IEnumerable<ConsoleRowHighlightingRule> DefaultRowHighlightingRules => new ConsoleRowHighlightingRule[] {

            new(){ Condition = "level == LogLevel.Trace",ForegroundColor = ConsoleOutputColor.Gray },
            new(){ Condition = "level == LogLevel.Fatal",ForegroundColor = ConsoleOutputColor.DarkRed },
            new(){ Condition = "level == LogLevel.Error",ForegroundColor = ConsoleOutputColor.Red },
            new(){ Condition = "level == LogLevel.Warning",ForegroundColor = ConsoleOutputColor.Yellow },
            new(){ Condition = "level == LogLevel.Info",ForegroundColor = ConsoleOutputColor.White }
        };

        private static IEnumerable<ConsoleWordHighlightingRule> DefaultWordHighlightingRules => new ConsoleWordHighlightingRule[] {

            new(){ Regex = LogLevel.Info.Name,ForegroundColor = ConsoleOutputColor.DarkGreen },
            new(){ Condition = "level == LogLevel.Debug",Text = "[TEST]",ForegroundColor = ConsoleOutputColor.Blue }
        };
    }
}
