namespace AtomicClock.Schedulers
{
    using System;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    public static class JobSchedulerHelper
    {
        public static void ScheduleJob<TTrigger>(this IJobScheduler jobScheduler, Action<dynamic, JobContext> jobAction, dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger
        {
            if (jobScheduler == null)
            {
                throw new ArgumentNullException(nameof(jobScheduler));
            }

            var jobActionOptions = new ActionJobOptions { Action = jobAction, Options = jobOptions };
            jobScheduler.ScheduleJob<ActionJob, TTrigger>(jobActionOptions, triggerOptions);
        }

        public static void ScheduleJob<TTrigger>(this IJobScheduler jobScheduler, Action<dynamic> jobAction, dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger
        {
            if (jobScheduler == null)
            {
                throw new ArgumentNullException(nameof(jobScheduler));
            }

            Action<dynamic, JobContext> action = (o, c) => { jobAction(o); };
            var jobActionOptions = new ActionJobOptions { Action = action, Options = jobOptions };
            jobScheduler.ScheduleJob<ActionJob, TTrigger>(jobActionOptions, triggerOptions);
        }

        public static void ScheduleJob<TTrigger>(this IJobScheduler jobScheduler, Action jobAction, dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger
        {
            if (jobScheduler == null)
            {
                throw new ArgumentNullException(nameof(jobScheduler));
            }

            Action<dynamic, JobContext> action = (o, c) => { jobAction(); };
            var jobActionOptions = new ActionJobOptions { Action = action, Options = jobOptions };
            jobScheduler.ScheduleJob<ActionJob, TTrigger>(jobActionOptions, triggerOptions);
        }
    }
}
