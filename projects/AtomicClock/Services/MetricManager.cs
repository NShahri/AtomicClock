// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricManager.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the MetricManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The metric manager.
    /// </summary>
    public class MetricManager
    {
        /// <summary>
        /// Gets or sets the metric aapter.
        /// </summary>
        /// <value>
        /// The metric aapter.
        /// </value>
        public static Action<string, TimeSpan, IEnumerable<string>> MetricAdapter { get; set; }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The disposable timer</returns>
        internal static IDisposable StartTimer(string name, IEnumerable<string> tags)
        {
            return new MetricsTimer(name, tags);
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The disposable timer</returns>
        internal static IDisposable StartTimer(string name, string tag)
        {
            return new MetricsTimer(name, new[] { tag });
        }

        /// <summary>
        /// Timers the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="elapsedMilliseconds">The elapsed milliseconds.</param>
        /// <param name="tags">The tags.</param>
        internal static void Timer(string name, TimeSpan elapsedMilliseconds, IEnumerable<string> tags)
        {
            MetricAdapter?.Invoke(name, elapsedMilliseconds, tags);
        }
    }
}
