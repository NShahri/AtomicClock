namespace AtomicClock.Schedulers
{
    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    public interface IJobScheduler
    {
        void ScheduleJob<TJob, TTrigger>(dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger where TJob : IJob;

        void Start();

        void Shutdown(bool waitForJobsToComplete);
    }
}
