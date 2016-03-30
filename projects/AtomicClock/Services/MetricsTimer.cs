// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricsTimer.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the MetricsTimer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The metrics timer.
    /// </summary>
    internal class MetricsTimer : IDisposable
    {
        /// <summary>
        /// The name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The tags.
        /// </summary>
        private readonly IEnumerable<string> tags;

        /// <summary>
        /// The stop watch.
        /// </summary>
        private readonly Stopwatch stopWatch;

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsTimer"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>
        public MetricsTimer(string name, IEnumerable<string> tags)
        {
            this.name = name;
            this.tags = tags;
            this.stopWatch = new Stopwatch();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(MetricsTimer));
            }

            this.disposed = true;

            this.stopWatch.Stop();
            MetricManager.Timer(this.name, this.stopWatch.Elapsed, this.tags);
        }
    }
}
