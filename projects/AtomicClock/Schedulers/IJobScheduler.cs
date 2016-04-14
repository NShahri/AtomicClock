// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJobScheduler.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   The JobScheduler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Schedulers
{
    using System;

    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    /// <summary>
    /// The JobScheduler interface.
    /// </summary>
    public interface IJobScheduler
    {
        /// <summary>
        /// The schedule job.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="triggerInfo">
        /// The trigger info.
        /// </param>
        void ScheduleJob(IJobInfo jobInfo, ITriggerInfo triggerInfo);

        /// <summary>
        /// Delete jobs from this scheduler.
        /// It does not affect the triggered jobs on queue to run.
        /// It does not affect the running jobs.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        void UnscheduleJob(Func<ITriggerInfo, IJobInfo, bool> predicate);

        /// <summary>
        /// It removes the triggered jobs from the queue.
        /// It tries to cancel running jobs.
        /// It does not affect the triggers in this scheduler.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        void DeleteJob(Func<IJobInfo, bool> predicate);

        /// <summary>
        /// The start.
        /// </summary>
        void Start();

        /// <summary>
        /// The shutdown.
        /// </summary>
        /// <param name="waitForJobsToComplete">
        /// The wait for jobs to complete.
        /// </param>
        void Shutdown(bool waitForJobsToComplete);
    }
}
