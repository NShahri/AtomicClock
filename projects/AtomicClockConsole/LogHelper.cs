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

    /// <summary>
    /// The log helper.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// The to n log level.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <returns>
        /// The <see cref="LogLevel"/>.
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
