// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerInfoValidator.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   The trigger info validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;

    using AtomicClock.Asserts;

    /// <summary>
    /// The trigger info validation.
    /// </summary>
    internal static class TriggerInfoValidator
    {
        /// <summary>
        /// The validate and throw.
        /// </summary>
        /// <param name="triggerInfo">
        /// The trigger info.
        /// </param>
        public static void ValidateAndThrow(this ITriggerInfo triggerInfo)
        {
            ArgumentAssert.NotNull(nameof(triggerInfo), triggerInfo);

            if (!typeof(ITrigger).IsAssignableFrom(triggerInfo.TriggerType))
            {
                throw new InvalidOperationException("Provided trigger type has to be based of ITrigger.");
            }
        }
    }
}
