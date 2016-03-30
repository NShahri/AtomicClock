// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogManager.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services
{
    using System;
    using System.Diagnostics;

    public class LogManager
    {
        public static Action<LogLevel, string, string, Exception> LogAdapter { get; set; }

        internal static LoggerService GetCurrentClassLogger()
        {
            var loggerName = new StackFrame(1, false).GetMethod()?.DeclaringType?.FullName ?? "NoName";
            return new LoggerService(loggerName);
        }

        internal static void Log(LogLevel level, string loggerName, string message, Exception ex)
        {
            LogAdapter?.Invoke(level, loggerName, message, ex);
        }
    }
}
