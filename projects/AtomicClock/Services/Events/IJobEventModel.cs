// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJobEventModel.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the IJobEventModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services.Events
{
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;

    /// <summary>
    /// The JobEventModel interface.
    /// </summary>
    public interface IJobEventModel
    {
        /// <summary>
        /// Gets the job info.
        /// </summary>
        IJobInfo JobInfo { get; }

        /// <summary>
        /// Gets the job scheduler.
        /// </summary>
        IJobScheduler JobScheduler { get; }
    }
}