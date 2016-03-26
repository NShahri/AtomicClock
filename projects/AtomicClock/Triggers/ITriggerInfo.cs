// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITriggerInfo.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the ITriggerInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;

    /// <summary>
    /// The LinkedCollectionCancellationTokenManager interface.
    /// </summary>
    public interface ITriggerInfo
    {
        /// <summary>
        /// Gets the trigger options.
        /// </summary>
        dynamic TriggerOptions { get; }

        /// <summary>
        /// Gets the trigger type.
        /// </summary>
        Type TriggerType { get; }

        /// <summary>
        /// Gets the trigger id.
        /// </summary>
        string TriggerId { get; }
    }
}