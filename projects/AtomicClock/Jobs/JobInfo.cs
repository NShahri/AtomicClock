﻿// --------------------------------------------------------------------------------------------------------------------
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

    using AtomicClock.QueueingPolicies;

    /// <summary>
    /// The job info.
    /// </summary>
    /// <typeparam name="TJob">
    /// the IJob implementation.
    /// </typeparam>
    public class JobInfo<TJob> : IJobInfo
        where TJob : IJob
    {
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
