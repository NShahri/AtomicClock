// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobScheduler.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the JobScheduler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Schedulers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Tasks;
    using AtomicClock.Triggers;

    /// <summary>
    /// The job scheduler.
    /// </summary>
    internal class JobScheduler : IJobScheduler
    {
        /// <summary>
        /// The maximum concurrency level.
        /// </summary>
        private readonly int maximumConcurrencyLevel;

        /// <summary>
        /// The schedule info.
        /// </summary>
        private readonly IDictionary<Guid, JobSchedulerInfo> scheduleInfo = new ConcurrentDictionary<Guid, JobSchedulerInfo>();

        /// <summary>
        /// The scheduler cancellation token source.
        /// </summary>
        private readonly CancellationTokenSource schedulerCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// The task scheduler.
        /// </summary>
        private CustomizedTaskScheduler taskScheduler;

        /// <summary>
        /// The started.
        /// </summary>
        private bool started;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobScheduler"/> class.
        /// </summary>
        /// <param name="maximumConcurrencyLevel">
        /// The maximum concurrency level.
        /// </param>
        public JobScheduler(int maximumConcurrencyLevel)
        {
            this.maximumConcurrencyLevel = maximumConcurrencyLevel;
        }

        /// <summary>
        /// The schedule job.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="triggerInfo">
        /// The trigger info.
        /// </param>
        public void ScheduleJob(IJobInfo jobInfo, ITriggerInfo triggerInfo)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(triggerInfo), triggerInfo);

            if (this.schedulerCancellationTokenSource.IsCancellationRequested)
            {
                throw new OperationCanceledException("Can not schedule a job in canceled job scheduler", this.schedulerCancellationTokenSource.Token);
            }

            jobInfo.ValidateAndThrow();
            triggerInfo.ValidateAndThrow();

            var info = new JobSchedulerInfo(triggerInfo, jobInfo, this.schedulerCancellationTokenSource.Token);
            this.scheduleInfo.Add(info.Id, info);

            if (this.started)
            {
                this.Start(info);
            }
        }

        /// <summary>
        /// Delete jobs from this scheduler.
        /// It does not affect the triggered jobs on queue to run.
        /// It does not affect the running jobs.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        public void UnscheduleJob(Func<ITriggerInfo, IJobInfo, bool> predicate)
        {
            ArgumentAssert.NotNull(nameof(predicate), predicate);

            foreach (var info in this.scheduleInfo.Values.Where(info => predicate(info.TriggerInfo, info.JobInfo)))
            {
                info.TriggerCancellationTokensManager.Cancel();
            }
        }

        /// <summary>
        /// It removes the triggered jobs from the queue.
        /// It tries to cancel running jobs.
        /// It does not affect the triggers in this scheduler.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        public void DeleteJob(Func<IJobInfo, bool> predicate)
        {
            ArgumentAssert.NotNull(nameof(predicate), predicate);

            foreach (var info in this.scheduleInfo.Values.Where(info => predicate(info.JobInfo)))
            {
                info.JobCancellationTokensManager.Cancel();
            }
        }

        /// <summary>
        /// The shutdown.
        /// </summary>
        /// <param name="waitForJobsToComplete">
        /// The wait for jobs to complete.
        /// </param>
        public void Shutdown(bool waitForJobsToComplete)
        {
            if (!this.started)
            {
                return;
            }

            this.started = false;

            this.schedulerCancellationTokenSource.Cancel();
            this.taskScheduler.Dispose();
            this.taskScheduler = null;
        }

        /// <summary>
        /// The start.
        /// </summary>
        public void Start()
        {
            if (this.started)
            {
                return;
            }

            this.started = true;

            this.InitScheduler();
            foreach (var info in this.scheduleInfo.Values)
            {
                this.Start(info);
            }
        }

        /// <summary>
        /// TO initialize scheduler.
        /// </summary>
        private void InitScheduler()
        {
            this.taskScheduler = new CustomizedTaskScheduler(this.maximumConcurrencyLevel);
        }

        /// <summary>
        /// The start.
        /// </summary>
        /// <param name="jobSchedulerInfo">
        /// The job scheduler info.
        /// </param>
        private void Start(JobSchedulerInfo jobSchedulerInfo)
        {
            var triggerInfo = jobSchedulerInfo.TriggerInfo;
            var triggerCancellationToken = jobSchedulerInfo.TriggerCancellationTokensManager.RegisterCancellationToken();

            var trigger = triggerInfo.CreateInstance();
            triggerCancellationToken.Register(
                () =>
                    {
                        this.scheduleInfo.Remove(jobSchedulerInfo.Id);
                    });

            var taskFactory = new CustomizedTaskFactory(this,  this.taskScheduler, jobSchedulerInfo.JobCancellationTokensManager);
            var triggerContext = new TriggerContext(triggerCancellationToken, taskFactory, this.taskScheduler);

            trigger.Schedule(jobSchedulerInfo.JobInfo, triggerContext);
        }
    }
}
