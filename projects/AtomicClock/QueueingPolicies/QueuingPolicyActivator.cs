﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueuingPolicyActivator.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   The queueing policy activator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.QueueingPolicies
{
    using System;

    /// <summary>
    /// The queuing policy activator.
    /// </summary>
    internal static class QueuingPolicyActivator
    {
        /// <summary>
        /// The create instance.
        /// </summary>
        /// <param name="queuingPolicy">
        /// The queuing policy.
        /// </param>
        /// <returns>
        /// The <see cref="IQueuingPolicy"/>.
        /// </returns>
        public static IQueuingPolicy CreateInstance(this QueuingPolicyInfo queuingPolicy)
        {
            return (IQueuingPolicy)Activator.CreateInstance(queuingPolicy.PolicyType, queuingPolicy.PolicyOptions);
        }
    }
}
