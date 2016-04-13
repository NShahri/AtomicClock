namespace AtomicClock.Tests.Triggers
{
    using System;

    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    using NSubstitute;

    using Xunit;

    public class TriggerInfoValidatorTest
    {
        [Fact]
        public void InvalidTriggerTypeTest()
        {
            var triggerInfo = Substitute.For<ITriggerInfo>();
            triggerInfo.TriggerType.Returns(_ => typeof(int));
            Assert.Throws<InvalidOperationException>(() => triggerInfo.ValidateAndThrow());
        }

        [Fact]
        public void NullTriggerTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((ITriggerInfo)null).ValidateAndThrow());
        }
    }
}
