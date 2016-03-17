namespace AtomicClock.Schedulers
{
    using System;

    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    internal class JobSchedulerInfo
    {
        public JobSchedulerInfo(Type triggerType, dynamic triggerOptions, Type jobType, dynamic jobOptions)
        {
            this.TriggerInfo = new TriggerInfo(triggerType, triggerOptions);    

            this.JobInfo = new JobInfo(jobType, jobOptions);
        }

        public TriggerInfo TriggerInfo { get; private set; }

        public JobInfo JobInfo { get; private set; }
    }
}
