namespace AtomicClock.Contexts
{
    using System.Threading;

    using AtomicClock.Schedulers;

    public class JobContext
    {
        public JobContext(CancellationToken cancellationToken, IJobScheduler jobScheduler)
        {
            this.CancellationToken = cancellationToken;
            this.JobScheduler = jobScheduler;
        }

        public CancellationToken CancellationToken { get; private set; }

        public IJobScheduler JobScheduler { get; private set; }
    }
}
