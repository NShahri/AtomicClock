// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerActivator.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the TriggerActivator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;

    /// <summary>
    /// The trigger activator.
    /// </summary>
    internal static class TriggerActivator
    {
        /// <summary>
        /// The create instance.
        /// </summary>
        /// <param name="triggerInfo">
        /// The trigger info.
        /// </param>
        /// <returns>
        /// The <see cref="ITrigger"/>.
        /// </returns>
        public static ITrigger CreateInstance(this ITriggerInfo triggerInfo)
        {
            return (ITrigger)Activator.CreateInstance(triggerInfo.TriggerType, triggerInfo.TriggerOptions);
        }
    }
}
