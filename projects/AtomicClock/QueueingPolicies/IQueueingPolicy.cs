namespace AtomicClock.QueueingPolicies
{
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Tasks;

    public interface IQueueingPolicy
    {
        bool CheckQueueingPolicy(JobInfo jobInfo, ITaskPool taskPool);
    }
}
