// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobSchedulerInfo.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the JobSchedulerInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Schedulers
{
    using System;
    using System.Threading;

    using AtomicClock.Asserts;
    using AtomicClock.CancellationTokens;
    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    /// <summary>
    /// The job scheduler info.
    /// </summary>
    internal class JobSchedulerInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobSchedulerInfo"/> class.
        /// </summary>
        /// <param name="triggerInfo">
        /// The trigger info.
        /// </param>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="schedulerCancellationToken">
        /// The scheduler Cancellation Token.
        /// </param>
        public JobSchedulerInfo(ITriggerInfo triggerInfo, IJobInfo jobInfo, CancellationToken schedulerCancellationToken)
        {
            ArgumentAssert.NotNull(nameof(triggerInfo), triggerInfo);
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(schedulerCancellationToken), schedulerCancellationToken);
            ArgumentAssert.NotCanceled(nameof(schedulerCancellationToken), schedulerCancellationToken);

            this.TriggerInfo = triggerInfo;
            this.JobInfo = jobInfo;
            this.Id = Guid.NewGuid();

            this.TriggerCancellationTokensManager = new LinkedCancellationTokensesManager(schedulerCancellationToken);
            this.JobCancellationTokensManager = this.JobInfo.ExecuteOnCancellation ?
                new CancellationTokensesManager() :
                new LinkedCancellationTokensesManager(schedulerCancellationToken);
        }

        /// <summary>
        /// Gets the trigger cancellation info.
        /// </summary>
        public ICancellationTokensManager TriggerCancellationTokensManager { get; private set; }

        /// <summary>
        /// Gets the job cancellation info.
        /// </summary>
        public ICancellationTokensManager JobCancellationTokensManager { get; private set; }

        /// <summary>
        /// Gets the trigger info.
        /// </summary>
        public ITriggerInfo TriggerInfo { get; private set; }

        /// <summary>
        /// Gets the job info.
        /// </summary>
        public IJobInfo JobInfo { get; private set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }
    }
}
