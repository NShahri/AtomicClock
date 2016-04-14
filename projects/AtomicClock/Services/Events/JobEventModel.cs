// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobEventModel.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the JobEventModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services.Events
{
    using AtomicClock.Asserts;
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;

    /// <summary>
    /// The job event model.
    /// </summary>
    public class JobEventModel : IJobEventModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobEventModel"/> class.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="jobScheduler">
        /// The job scheduler.
        /// </param>
        public JobEventModel(IJobInfo jobInfo, IJobScheduler jobScheduler)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(jobScheduler), jobScheduler);

            this.JobInfo = jobInfo;
            this.JobScheduler = jobScheduler;
        }

        /// <summary>
        /// Gets the job info.
        /// </summary>
        public IJobInfo JobInfo { get; private set; }

        /// <summary>
        /// Gets the job scheduler.
        /// </summary>
        public IJobScheduler JobScheduler { get; private set; }
    }
}
