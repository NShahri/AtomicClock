// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomizedTask.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   The customized task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Tasks
{
    using System;
    using System.Threading.Tasks;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Services;

    /// <summary>
    /// The customized task.
    /// </summary>
    internal class CustomizedTask : Task
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly LoggerService Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizedTask"/> class.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="jobContext">
        /// The job context.
        /// </param>
        public CustomizedTask(IJobInfo jobInfo, JobContext jobContext)
            : base(MakeAction(jobInfo, jobContext), jobContext.CancellationToken)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(jobContext), jobContext);

            EventManager.JobQueued(jobInfo, jobContext.JobScheduler);

            jobContext.CancellationToken.Register(
                () => EventManager.JobCancelled(jobInfo, jobContext.JobScheduler));

            this.JobInfo = jobInfo;
        }

        /// <summary>
        /// Gets the job info.
        /// </summary>
        public IJobInfo JobInfo { get; private set; }

        /// <summary>
        /// Making action which will be used as task action.
        /// This action measures the time which is in queue.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="jobContext">
        /// The job context.
        /// </param>
        /// <returns>
        /// The <see cref="Action"/>.
        /// </returns>
        private static Action MakeAction(IJobInfo jobInfo, JobContext jobContext)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(jobContext), jobContext);

            var queuingMetricTimer = MetricManager.StartTimer("queuing.duration", $"job.Id: {jobInfo.JobId}");

            return
                () =>
                    {
                        queuingMetricTimer.Dispose();
                        RunJob(jobInfo, jobContext);
                    };
        }

        /// <summary>
        /// The run job.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="jobContext">
        /// The job context.
        /// </param>
        private static void RunJob(IJobInfo jobInfo, JobContext jobContext)
        {
            EventManager.JobRunning(jobInfo, jobContext.JobScheduler);

            try
            {
                var job = jobInfo.CreateInstance();
                using (MetricManager.StartTimer("job.duration", $"job.Id: {jobInfo.JobId}"))
                {
                    job.Execute(jobContext);
                }
            }
            catch (Exception ex)
            {
                // unexpected internal exception in user code
                // Ignore this exception
                EventManager.JobException(jobInfo, jobContext.JobScheduler);
                Logger.Error($"An exception has occurred running {jobInfo.JobId}", ex);
            }
            finally
            {
                EventManager.JobCompleted(jobInfo, jobContext.JobScheduler);
            }
        }
    }
}
