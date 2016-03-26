// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITrigger.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the ITrigger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    /// <summary>
    /// The Trigger interface.
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// The schedule.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        void Schedule(IJobInfo jobInfo, TriggerContext context);
    }
}
