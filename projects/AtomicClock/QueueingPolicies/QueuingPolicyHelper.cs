// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueuingPolicyHelper.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   The queuing policy helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.QueueingPolicies
{
    using System.Linq;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    /// <summary>
    /// The queuing policy helper.
    /// </summary>
    internal static class QueuingPolicyHelper
    {
        /// <summary>
        /// The check all queuing policies.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckAllQueuingPolicies(this IJobInfo jobInfo, TriggerContext context)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(context), context);

            var policies = jobInfo.QueuingPolicies;
            if (policies == null)
            {
                return true;
            }

            return policies.Select(executionPolicy => executionPolicy.CreateInstance())
                .All(policy => policy.CheckQueuingPolicy(jobInfo, context.TaskPool));
        }
    }
}
