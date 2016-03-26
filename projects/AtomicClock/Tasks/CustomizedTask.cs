// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomizedTask.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
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
            : base(() => RunJob(jobInfo, jobContext), jobContext.CancellationToken)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(jobContext), jobContext);

            this.JobInfo = jobInfo;
        }

        /// <summary>
        /// Gets the job info.
        /// </summary>
        public IJobInfo JobInfo { get; private set; }
        
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
            var job = jobInfo.CreateInstance();
            try
            {
                job.Execute(jobContext);
            }
            catch (Exception ex)
            {
                // unexpected exception, IGNORE IT
                Logger.Error("Job execution exception", ex);
            }
        }
    }
}
