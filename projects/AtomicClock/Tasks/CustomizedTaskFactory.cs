// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomizedTaskFactory.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the CustomizedTaskFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Tasks
{
    using System.Threading.Tasks;

    using AtomicClock.Asserts;
    using AtomicClock.CancellationTokens;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;

    /// <summary>
    /// The customized task factory.
    /// </summary>
    internal class CustomizedTaskFactory : ITaskFactory
    {
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task StartNew(IJobInfo jobInfo)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);

            var cancellationToken = this.jobCancellationTokensManager.RegisterCancellationToken();
            var jobContext = new JobContext(cancellationToken, this.jobScheduler);

            var task = new CustomizedTask(jobInfo, jobContext);
            task.ContinueWith(
                t =>
                    {
                        this.jobCancellationTokensManager.UnregisterCancellationToken(cancellationToken);
                    }, 
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.NotOnCanceled);
            task.Start(this.taskScheduler);

            return task;
        }
    }
}
