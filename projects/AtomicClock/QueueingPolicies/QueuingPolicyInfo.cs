// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueuingPolicyInfo.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the QueuingPolicyInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.QueueingPolicies
{
    using System;

    /// <summary>
    /// The queuing policy info.
    /// </summary>
    public class QueuingPolicyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueuingPolicyInfo"/> class.
        /// </summary>
        /// <param name="policyType">
        /// The policy type.
        /// </param>
        /// <param name="policyOptions">
        /// The policy options.
        /// </param>
        public QueuingPolicyInfo(Type policyType, dynamic policyOptions)
        {
            this.PolicyType = policyType;
            this.PolicyOptions = policyOptions;
        }

        /// <summary>
        /// Gets the policy type.
        /// </summary>
        public Type PolicyType { get; private set; }

        /// <summary>
        /// Gets the policy options.
        /// </summary>
        public dynamic PolicyOptions { get; private set; }
    }
}
