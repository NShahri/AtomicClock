// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventManager.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the EventManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services
{
    using System;

    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;
    using AtomicClock.Services.Events;

    /// <summary>
    /// The event manager.
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// Gets or sets the job event adapter.
        /// </summary>
        public static Action<JobEvents, IJobEventModel> JobEventAdapter { get; set; }

        internal static void JobQueued(IJobInfo jobInfo, IJobScheduler jobScheduler)
        {
            JobEventAdapter?.Invoke(JobEvents.Queued, new JobEventModel(jobInfo, jobScheduler));
        }

        internal static void JobRunning(IJobInfo jobInfo, IJobScheduler jobScheduler)
        {
            JobEventAdapter?.Invoke(JobEvents.Running, new JobEventModel(jobInfo, jobScheduler));
        }

        internal static void JobCompleted(IJobInfo jobInfo, IJobScheduler jobScheduler)
        {
            JobEventAdapter?.Invoke(JobEvents.Completed, new JobEventModel(jobInfo, jobScheduler));
        }

        internal static void JobCancelled(IJobInfo jobInfo, IJobScheduler jobScheduler)
        {
            JobEventAdapter?.Invoke(JobEvents.Cancelled, new JobEventModel(jobInfo, jobScheduler));
        }

        internal static void JobException(IJobInfo jobInfo, IJobScheduler jobScheduler)
        {
            JobEventAdapter?.Invoke(JobEvents.ExecutionException, new JobEventModel(jobInfo, jobScheduler));
        }
    }
}
