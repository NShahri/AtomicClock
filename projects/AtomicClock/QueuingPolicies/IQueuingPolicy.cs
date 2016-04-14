// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueuingPolicy.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the IQueuingPolicy type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.QueuingPolicies
{
    using AtomicClock.Jobs;
    using AtomicClock.Tasks;

    /// <summary>
    /// The QueuingPolicy interface.
    /// </summary>
    public interface IQueuingPolicy
    {
        /// <summary>
        /// The check queuing policy.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="taskPool">
        /// The task pool.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool CheckQueuingPolicy(IJobInfo jobInfo, ITaskPool taskPool);
    }
}
