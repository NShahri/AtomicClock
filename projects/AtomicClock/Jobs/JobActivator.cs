namespace AtomicClock.Jobs
{
    using System;

    internal static class JobActivator
    {
        public static IJob CreateInstance(this JobInfo jobInfo)
        {
            return (IJob)Activator.CreateInstance(jobInfo.JobType, jobInfo.JobOptions);
        }
    }
}
