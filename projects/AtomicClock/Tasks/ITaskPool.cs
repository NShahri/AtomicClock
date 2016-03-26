// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITaskPool.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the ITaskPool type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Tasks
{
    using System.Collections.Generic;

    using AtomicClock.Jobs;

    /// <summary>
    /// The TaskPool interface.
    /// </summary>
    public interface ITaskPool
    {
        /// <summary>
        /// The get tasks.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IJobInfo}"/>.
        /// </returns>
        IEnumerable<IJobInfo> GetTasks();
    }
}
