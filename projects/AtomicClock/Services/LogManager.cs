// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogManager.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The log manager.
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// Gets or sets the log adapter.
        /// </summary>
        /// <value>
        /// The log adapter.
        /// </value>
        public static Action<LogLevel, string, string, Exception> LogAdapter { get; set; }

        /// <summary>
        /// Gets the current class logger.
        /// </summary>
        /// <returns>The logger service</returns>
        internal static LoggerService GetCurrentClassLogger()
        {
            var loggerName = new StackFrame(1, false).GetMethod()?.DeclaringType?.FullName ?? "NoName";
            return new LoggerService(loggerName);
        }

        /// <summary>
        /// Logs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="loggerName">Name of the logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        internal static void Log(LogLevel level, string loggerName, string message, Exception ex)
        {
            LogAdapter?.Invoke(level, loggerName, message, ex);
        }
    }
}
