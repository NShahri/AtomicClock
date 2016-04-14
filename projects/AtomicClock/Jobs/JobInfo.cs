// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobInfo.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the triggerInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace AtomicClock.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AtomicClock.QueuingPolicies;

    /// <summary>
    /// The job info.
    /// </summary>
    /// <typeparam name="TJob">
    /// the IJob implementation.
    /// </typeparam>
    public class JobInfo<TJob> : IJobInfo
        where TJob : IJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobInfo{TJob}"/> class.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="queuingPolicyInfo">The queuing policy information.</param>
        /// <param name="executeOnCancellation">if set to <c>true</c> [execute on cancellation].</param>
        /// <param name="jobOptions">The job options.</param>
        public JobInfo(
            string jobId = null,
            IEnumerable<QueuingPolicyInfo> queuingPolicyInfo = null,
            bool executeOnCancellation = false,
            dynamic jobOptions = null)
        {
            this.JobOptions = jobOptions;
            this.JobId = jobId ?? Guid.NewGuid().ToString();
            this.ExecuteOnCancellation = executeOnCancellation;
            if (queuingPolicyInfo != null)
            {
                this.QueuingPolicies = queuingPolicyInfo.ToList();
            }
        }

        /// <inheritdoc/>
        public Type JobType => typeof(TJob);

        /// <inheritdoc/>
        public dynamic JobOptions { get; }

        /// <inheritdoc/>
        public IEnumerable<QueuingPolicyInfo> QueuingPolicies { get; }

        /// <inheritdoc/>
        public string JobId { get; }

        /// <inheritdoc/>
        public bool ExecuteOnCancellation { get; }
    }
}
