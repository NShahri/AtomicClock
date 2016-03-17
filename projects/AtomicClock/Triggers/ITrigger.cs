namespace AtomicClock.Triggers
{
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    public interface ITrigger
    {
        void Schedule(JobInfo jobInfo, TriggerContext context);
    }
}
