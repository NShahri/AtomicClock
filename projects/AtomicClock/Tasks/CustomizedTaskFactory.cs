// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomizedTaskFactory.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the CustomizedTaskFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AtomicClock.Asserts;
    using AtomicClock.CancellationTokens;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;
    using AtomicClock.Services;

    /// <summary>
    /// The customized task factory.
    /// </summary>
    internal class CustomizedTaskFactory : ITaskFactory
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly LoggerService Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The job cancellation info.
        /// </summary>
        private readonly ICancellationTokensManager jobCancellationTokensManager;

        /// <summary>
        /// The job scheduler.
        /// </summary>
        private readonly IJobScheduler jobScheduler;

        /// <summary>
        /// The task scheduler.
        /// </summary>
        private readonly TaskScheduler taskScheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizedTaskFactory"/> class.
        /// </summary>
        /// <param name="jobScheduler">
        /// The job scheduler.
        /// </param>
        /// <param name="taskScheduler">
        /// The task scheduler.
        /// </param>
        /// <param name="jobCancellationTokensManager">
        /// The job Cancellation Info.
        /// </param>
        public CustomizedTaskFactory(IJobScheduler jobScheduler, TaskScheduler taskScheduler, ICancellationTokensManager jobCancellationTokensManager)
        {
            ArgumentAssert.NotNull(nameof(jobScheduler), jobScheduler);
            ArgumentAssert.NotNull(nameof(taskScheduler), taskScheduler);
            ArgumentAssert.NotNull(nameof(jobCancellationTokensManager), jobCancellationTokensManager);

            this.jobScheduler = jobScheduler;
            this.taskScheduler = taskScheduler;
            this.jobCancellationTokensManager = jobCancellationTokensManager;
        }

        /// <summary>
        /// The start new.
        /// </summary>
        /// <param name="jobInfo">
        /// The job Info.
        /// </param>
        public void StartNew(IJobInfo jobInfo)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);

            var cancellationToken = this.jobCancellationTokensManager.RegisterCancellationToken();

            var jobContext = this.GetJobContext(cancellationToken);
            if (jobContext == null)
            {
                return;
            }

            var task = new CustomizedTask(jobInfo, jobContext);
            task.ContinueWith(
                _ => this.jobCancellationTokensManager.UnregisterCancellationToken(cancellationToken),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.NotOnCanceled);

            this.StartTask(task);
        }

        /// <summary>
        /// The get job context.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="JobContext"/>.
        /// </returns>
        protected JobContext GetJobContext(CancellationToken token)
        {
            try
            {
                return new JobContext(token, this.jobScheduler);
            }
            catch (OperationCanceledException ex)
            {
                // As there is a small time between creating task (with connected token)
                // and starting task (adding to task scheduler)
                // there is a chance user cancells the token before running task
                // DO NOTHING in this case is a valid policy
                Logger.Info(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The start task.
        /// </summary>
        /// <param name="task">
        /// The task.
        /// </param>
        protected void StartTask(Task task)
        {
            try
            {
                task.Start(this.taskScheduler);
            }
            catch (InvalidOperationException ex)
            {
                // As there is a small time between creating task (with connected token)
                // and starting task (adding to task scheduler)
                // there is a chance user cancells the token before running task
                // DO NOTHING in this case is a valid policy
                Logger.Info(ex.Message);
            }
        }
    }
}
