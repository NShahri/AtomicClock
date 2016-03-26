﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITaskFactory.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   The TaskFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Tasks
{
    using System.Threading.Tasks;

    using AtomicClock.Jobs;

    /// <summary>
    /// The TaskFactory interface.
    /// </summary>
    public interface ITaskFactory
    {
        /// <summary>
        /// The start new.
        /// </summary>
        /// <param name="jobInfo">
        /// The job Info.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task StartNew(IJobInfo jobInfo);
    }
}
