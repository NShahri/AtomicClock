// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunNowTrigger.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the RunNowTrigger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    /// <summary>
    /// The run now trigger.
    /// </summary>
    public class RunNowTrigger : ITrigger
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
        public void Schedule(IJobInfo jobInfo, TriggerContext context)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(context), context);

            context.TaskFactory.StartNew(jobInfo);
        }
    }
}
