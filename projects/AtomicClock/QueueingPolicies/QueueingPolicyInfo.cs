namespace AtomicClock.QueueingPolicies
{
    using System;

    public class QueueingPolicyInfo
    {
        public QueueingPolicyInfo(Type policyType, dynamic policyOptions)
        {
            this.PolicyType = policyType;
            this.PolicyOptions = policyOptions;
        }

        public Type PolicyType { get; private set; }

        public dynamic PolicyOptions { get; private set; }
    }
}
