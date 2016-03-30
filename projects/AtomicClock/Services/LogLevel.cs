// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogLevel.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the LogLevel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services
{
    /// <summary>
    /// The log level.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// The trace.
        /// </summary>
        Trace = 1,

        /// <summary>
        /// The debug.
        /// </summary>
        Debug = 2,

        /// <summary>
        /// The info.
        /// </summary>
        Info = 4,

        /// <summary>
        /// The warn.
        /// </summary>
        Warn = 8,

        /// <summary>
        /// The error.
        /// </summary>
        Error = 16,

        /// <summary>
        /// The fatal.
        /// </summary>
        Fatal = 32
    }
}
