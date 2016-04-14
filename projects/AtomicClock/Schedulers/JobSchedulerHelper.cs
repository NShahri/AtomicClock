// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobSchedulerHelper.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the JobSchedulerHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Schedulers
{
    using System;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    /// <summary>
    /// The job scheduler helper.
    /// </summary>
    public static class JobSchedulerHelper
    {
        /// <summary>
        /// The schedule job.
        /// </summary>
        /// <param name="jobScheduler">
        /// The job scheduler.
        /// </param>
        /// <param name="jobOptions">
        /// The job options.
        /// </param>
        /// <param name="triggerOptions">
        /// The trigger options.
        /// </param>
        /// <typeparam name="TJob">
        /// the IJob implementation.
        /// </typeparam>
        /// <typeparam name="TTrigger">
        /// the ITrigger implementation.
        /// </typeparam>
        public static void ScheduleJob<TJob, TTrigger>(this IJobScheduler jobScheduler, dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger
            where TJob : IJob
        {
            ArgumentAssert.NotNull(nameof(jobScheduler), jobScheduler);

            var jobInfo = new JobInfo<TJob>(jobOptions: jobOptions);
            var triggerInfo = new TriggerInfo<TTrigger>(triggerOptions: triggerOptions);
            jobScheduler.ScheduleJob(jobInfo, triggerInfo);
        }

        /// <summary>
        /// The schedule job.
        /// </summary>
        /// <param name="jobScheduler">
        /// The job scheduler.
        /// </param>
        /// <param name="jobAction">
        /// The job action.
        /// </param>
        /// <param name="jobOptions">
        /// The job options.
        /// </param>
        /// <param name="triggerOptions">
        /// The trigger options.
        /// </param>
        /// <typeparam name="TTrigger">
        /// the ITrigger implementation.
        /// </typeparam>
        public static void ScheduleJob<TTrigger>(this IJobScheduler jobScheduler, Action<dynamic, JobContext> jobAction, dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger
        {
            ArgumentAssert.NotNull(nameof(jobScheduler), jobScheduler);

            var jobActionOptions = new ActionJobOptions { Action = jobAction, Options = jobOptions };
            var jobInfo = new JobInfo<ActionJob>(jobOptions: jobActionOptions);
            var triggerInfo = new TriggerInfo<TTrigger>(triggerOptions: triggerOptions);
            jobScheduler.ScheduleJob(jobInfo, triggerInfo);
        }

        /// <summary>
        /// The schedule job.
        /// </summary>
        /// <param name="jobScheduler">
        /// The job scheduler.
        /// </param>
        /// <param name="jobAction">
        /// The job action.
        /// </param>
        /// <param name="jobOptions">
        /// The job options.
        /// </param>
        /// <param name="triggerOptions">
        /// The trigger options.
        /// </param>
        /// <typeparam name="TTrigger">
        /// the ITrigger implementation.
        /// </typeparam>
        public static void ScheduleJob<TTrigger>(this IJobScheduler jobScheduler, Action<dynamic> jobAction, dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger
        {
            ArgumentAssert.NotNull(nameof(jobScheduler), jobScheduler);

            Action<dynamic, JobContext> action = (o, c) => { jobAction(o); };
            var jobActionOptions = new ActionJobOptions { Action = action, Options = jobOptions };
            var jobInfo = new JobInfo<ActionJob>(jobOptions: jobActionOptions);
            var triggerInfo = new TriggerInfo<TTrigger>(triggerOptions: triggerOptions);
            jobScheduler.ScheduleJob(jobInfo, triggerInfo);
        }

        /// <summary>
        /// The schedule job.
        /// </summary>
        /// <param name="jobScheduler">
        /// The job scheduler.
        /// </param>
        /// <param name="jobAction">
        /// The job action.
        /// </param>
        /// <param name="triggerOptions">
        /// The trigger options.
        /// </param>
        /// <typeparam name="TTrigger">
        /// the ITrigger implementation.
        /// </typeparam>
        public static void ScheduleJob<TTrigger>(this IJobScheduler jobScheduler, Action jobAction, dynamic triggerOptions = null)
            where TTrigger : ITrigger
        {
            ArgumentAssert.NotNull(nameof(jobScheduler), jobScheduler);

            Action<dynamic, JobContext> action = (o, c) => { jobAction(); };
            var jobActionOptions = new ActionJobOptions { Action = action };
            var jobInfo = new JobInfo<ActionJob>(jobOptions: jobActionOptions);
            var triggerInfo = new TriggerInfo<TTrigger>(triggerOptions: triggerOptions);
            jobScheduler.ScheduleJob(jobInfo, triggerInfo);
        }
    }
}
