// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerService.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the LoggerService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services
{
    using System;

    /// <summary>
    /// The logger service.
    /// </summary>
    internal class LoggerService
    {
        /// <summary>
        /// The logger name.
        /// </summary>
        private readonly string loggerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerService"/> class.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public LoggerService(string loggerName)
        {
            this.loggerName = loggerName;
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public void Error(string message, Exception ex)
        {
            LogManager.Log(LogLevel.Error, this.loggerName, message, ex);
        }

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public void Fatal(string message, Exception ex)
        {
            LogManager.Log(LogLevel.Fatal, this.loggerName, message, ex);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Debug(string message)
        {
            LogManager.Log(LogLevel.Debug, this.loggerName, message, null);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Info(string message)
        {
            LogManager.Log(LogLevel.Info, this.loggerName, message, null);
        }
    }
}
