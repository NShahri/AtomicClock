namespace AtomicClock.QueueingPolicies
{
    using System.Linq;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Tasks;

    public class DisallowConcurrentQueueingPolicy : IQueueingPolicy
    {
        public static QueueingPolicyInfo GetInfo()
        {
            return new QueueingPolicyInfo(typeof(DisallowConcurrentQueueingPolicy), null);
        }

        public bool CheckQueueingPolicy(JobInfo jobInfo, ITaskPool taskPool)
        {
            var tasks = taskPool.GetTasks();
            return tasks.All(t => t.JobId != jobInfo.JobId);
        }
    }
}
