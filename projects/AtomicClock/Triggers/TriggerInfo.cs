// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerInfo.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;

    public class TriggerInfo<TTrigger> : ITriggerInfo
        where TTrigger : ITrigger
    {
        public TriggerInfo(string triggerId = null, dynamic triggerOptions = null)
        {
            this.TriggerOptions = triggerOptions;
            this.TriggerId = triggerId;
        }

        public Type TriggerType => typeof(TTrigger);

        public string TriggerId { get; }

        public dynamic TriggerOptions { get; }
    }
}
