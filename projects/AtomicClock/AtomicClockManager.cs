namespace AtomicClock
{
    using global::AtomicClock.Schedulers;

    public class AtomicClockManager
    {
        public static IJobScheduler CreateScheduler(int threadCount)
        {
            return new JobScheduler(threadCount);
        }
    }
}
