namespace AtomicClock.Jobs
{
    using System;

    using AtomicClock.Contexts;

    internal class ActionJobOptions
    {
        public Action<dynamic, JobContext> Action { get; set; }

        public dynamic Options { get; set; }
    }
}