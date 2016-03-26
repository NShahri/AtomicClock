// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisallowConcurrentQueuingPolicy.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the DisallowConcurrentQueuingPolicy type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.QueueingPolicies
{
    using System.Linq;

    using AtomicClock.Jobs;
    using AtomicClock.Tasks;

    /// <summary>
    /// The disallow concurrent queuing policy.
    /// </summary>
    public class DisallowConcurrentQueuingPolicy : IQueuingPolicy
    {
        /// <summary>
        /// The get info.
        /// </summary>
        /// <returns>
        /// The <see cref="QueuingPolicyInfo"/>.
        /// </returns>
        public static QueuingPolicyInfo GetInfo()
        {
            return new QueuingPolicyInfo(typeof(DisallowConcurrentQueuingPolicy), null);
        }

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
        public bool CheckQueuingPolicy(IJobInfo jobInfo, ITaskPool taskPool)
        {
            var tasks = taskPool.GetTasks();
            return tasks.All(t => t.JobId != jobInfo.JobId);
        }
    }
}
