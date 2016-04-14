// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the LogHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService
{
    using System;

    using AtomicClock.Services;

    using NLog.Conditions;
    using NLog.Config;
    using NLog.Targets;

    /// <summary>
    /// The log helper.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// The NLOG to console.
        /// </summary>
        public static void NLogToConsole()
        {
            var consoleTarget = new ColoredConsoleTarget();

            var highlightRule = new ConsoleRowHighlightingRule
                {
                    Condition = ConditionParser.ParseExpression("level == LogLevel.Info"),
                    ForegroundColor = ConsoleOutputColor.Green
                };

            var highlightRule2 = new ConsoleRowHighlightingRule
                {
                    Condition = ConditionParser.ParseExpression("level == LogLevel.Debug"),
                    ForegroundColor = ConsoleOutputColor.DarkGreen
                };

            consoleTarget.RowHighlightingRules.Add(highlightRule);
            consoleTarget.RowHighlightingRules.Add(highlightRule2);

            var config = new LoggingConfiguration();
            config.AddTarget("console", consoleTarget);

            var rule1 = new LoggingRule("*", NLog.LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);

            NLog.LogManager.Configuration = config;
        }

        /// <summary>
        /// The to n log level.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <returns>
        /// The <see cref="NLog.LogLevel"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When there is no map defined between AtomicClock.LogLevel and NLog.LogLevel
        /// </exception>
        public static NLog.LogLevel ToNLogLevel(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
    }
}
