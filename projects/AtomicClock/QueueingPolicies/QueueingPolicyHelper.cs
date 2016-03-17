namespace AtomicClock.QueueingPolicies
{
    using System;
    using System.Linq;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    internal static class QueueingPolicyHelper
    {
        public static bool CheckAllQueueingPolicies(this JobInfo jobInfo, TriggerContext context)
        {
            if (jobInfo == null)
            {
                throw new ArgumentNullException(nameof(jobInfo));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var policies = jobInfo.QueueingPolicies;
            if (policies == null)
            {
                return true;
            }

            return policies.Select(executionPolicy => executionPolicy.CreateInstance())
                .All(policy => policy.CheckQueueingPolicy(jobInfo, context.TaskPool));
        }
    }
}
