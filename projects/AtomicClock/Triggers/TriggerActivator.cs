namespace AtomicClock.Triggers
{
    using System;

    internal static class TriggerActivator
    {
        public static ITrigger CreateInstance(this TriggerInfo triggerInfo)
        {
            return (ITrigger)Activator.CreateInstance(triggerInfo.TriggerType, triggerInfo.TriggerOptions);
        }
    }
}
