namespace AtomicClock.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class MetricsTimer : IDisposable
    {
        private readonly string name;

        private readonly IEnumerable<string> tags;

        private readonly Stopwatch stopWatch;

        private bool disposed;

        public MetricsTimer(string name, IEnumerable<string> tags)
        {
            this.name = name;
            this.tags = tags;
            this.stopWatch = new Stopwatch();
        }

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
