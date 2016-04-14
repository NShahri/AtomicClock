// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtomicClockManager.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the AtomicClockManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock
{
    using global::AtomicClock.Schedulers;

    /// <summary>
    /// The atomic clock manager.
    /// </summary>
    public static class AtomicClockManager
    {
        /// <summary>
        /// The create scheduler.
        /// </summary>
        /// <param name="threadCount">
        /// The thread count.
        /// </param>
        /// <returns>
        /// The <see cref="IJobScheduler"/>.
        /// </returns>
        public static IJobScheduler CreateScheduler(int threadCount)
        {
            return new JobScheduler(threadCount);
        }
    }
}
