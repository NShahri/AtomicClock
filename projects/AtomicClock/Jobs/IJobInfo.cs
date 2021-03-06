﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJobInfo.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the IJobInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Jobs
{
    using System;
    using System.Collections.Generic;

    using AtomicClock.QueuingPolicies;

    /// <summary>
    /// The triggerInfo interface.
    /// </summary>
    public interface IJobInfo
    {
        /// <summary>
        /// Gets a value indicating whether execute on cancellation.
        /// </summary>
        bool ExecuteOnCancellation { get; }

        /// <summary>
        /// Gets the job id.
        /// </summary>
        string JobId { get; }

        /// <summary>
        /// Gets the job options.
        /// </summary>
        dynamic JobOptions { get; }

        /// <summary>
        /// Gets the job type.
        /// </summary>
        Type JobType { get; }

        /// <summary>
        /// Gets the queuing policies.
        /// </summary>
        IEnumerable<QueuingPolicyInfo> QueuingPolicies { get; }
    }
}