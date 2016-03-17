namespace AtomicClock.Triggers
{
    using System;
    using System.Collections.Generic;

    using AtomicClock.QueueingPolicies;

    internal class TriggerInfo
    {
        public TriggerInfo(Type triggerType, dynamic triggerOptions)
        {
            this.TriggerType = triggerType;
            this.TriggerOptions = triggerOptions;
        }

        public Type TriggerType { get; private set; }

        public dynamic TriggerOptions { get; private set; }
    }
}
