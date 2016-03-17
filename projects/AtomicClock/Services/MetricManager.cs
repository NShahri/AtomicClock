namespace AtomicClock.Services
{
    using System;
    using System.Collections.Generic;

    public class MetricManager
    {
        public static Action<string, TimeSpan, IEnumerable<string>> MetricAapter { get; set; }

        internal static IDisposable StartTimer(string name, IEnumerable<string> tags)
        {
            return new MetricsTimer(name, tags);
        }

        internal static IDisposable StartTimer(string name, string tag)
        {
            return new MetricsTimer(name, new [] { tag });
        }

        internal static void Timer(string name, TimeSpan elapsedMilliseconds, IEnumerable<string> tags)
        {
            MetricAapter?.Invoke(name, elapsedMilliseconds, tags);
        }
    }
}
