namespace AtomicClock.Triggers
{
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    public class RunNowTrigger : ITrigger
    {
        public void Schedule(JobInfo jobInfo, TriggerContext context)
        {
            context.TaskFactory.StartNew(jobInfo);
        }
    }
}
