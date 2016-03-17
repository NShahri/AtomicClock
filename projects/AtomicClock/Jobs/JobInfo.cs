namespace AtomicClock.Jobs
{
    using System;
    using System.Collections.Generic;

    using AtomicClock.QueueingPolicies;

    public class JobInfo
    {
        public JobInfo(Type jobType, dynamic jobOptions)
        {
            this.JobType = jobType;
            this.JobOptions = jobOptions;
            this.JobId = Guid.NewGuid().ToString();
            this.QueueingPolicies = null;// new List<Type>() { typeof(AllowConcurrentExecutionPolicy) };
            this.ExecuteOnCancellation = false;
        }

        public Type JobType { get; private set; }

        public dynamic JobOptions { get; private set; }

        public IEnumerable<QueueingPolicyInfo> QueueingPolicies { get; private set; }

        public string JobId { get; private set; }

        //It describes on cancellation request what is the behavior for all queued jobs
        public bool ExecuteOnCancellation { get; private set; }
    }
}
