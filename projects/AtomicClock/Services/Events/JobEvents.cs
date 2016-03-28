// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobEvents.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the JobEvents type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Services.Events
{
    /// <summary>
    /// The job events.
    /// </summary>
    public enum JobEvents
    {
        /// <summary>
        /// The job is queued.
        /// </summary>
        Queued,

        /// <summary>
        /// The job is running.
        /// </summary>
        Running,

        /// <summary>
        /// The job is completed.
        /// </summary>
        Completed,

        /// <summary>
        /// The job is cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The execution exception.
        /// </summary>
        ExecutionException
    }
}
