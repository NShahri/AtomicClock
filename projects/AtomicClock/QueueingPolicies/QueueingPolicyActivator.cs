namespace AtomicClock.QueueingPolicies
{
    using System;

    internal static class QueueingPolicyActivator
    {
        public static IQueueingPolicy CreateInstance(this QueueingPolicyInfo queueingPolicy)
        {
            return (IQueueingPolicy)Activator.CreateInstance(queueingPolicy.PolicyType, queueingPolicy.PolicyOptions);
        }
    }
}
