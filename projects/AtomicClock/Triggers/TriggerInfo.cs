// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerInfo.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;

    /// <summary>
    /// The trigger info.
    /// </summary>
    /// <typeparam name="TTrigger">The type of the trigger.</typeparam>
    /// <seealso cref="AtomicClock.Triggers.ITriggerInfo" />
    public class TriggerInfo<TTrigger> : ITriggerInfo
        where TTrigger : ITrigger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerInfo{TTrigger}"/> class.
        /// </summary>
        /// <param name="triggerId">The trigger identifier.</param>
        /// <param name="triggerOptions">The trigger options.</param>
        public TriggerInfo(string triggerId = null, dynamic triggerOptions = null)
        {
            this.TriggerOptions = triggerOptions;
            this.TriggerId = triggerId;
        }

        /// <summary>
        /// Gets the trigger type.
        /// </summary>
        public Type TriggerType => typeof(TTrigger);

        /// <summary>
        /// Gets the trigger id.
        /// </summary>
        public string TriggerId { get; }

        /// <summary>
        /// Gets the trigger options.
        /// </summary>
        public dynamic TriggerOptions { get; }
    }
}
