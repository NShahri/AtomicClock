// <copyright file="TriggerInfoValidatorTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.Triggers
{
    using System;

    using AtomicClock.Jobs;
    using AtomicClock.Triggers;

    using NSubstitute;

    using Xunit;

    /// <summary>
    /// Tests for trigger info validator.
    /// </summary>
    public class TriggerInfoValidatorTest
    {
        /// <summary>
        /// Invalids the trigger type test.
        /// </summary>
        [Fact]
        public void InvalidTriggerTypeTest()
        {
            var triggerInfo = Substitute.For<ITriggerInfo>();
            triggerInfo.TriggerType.Returns(_ => typeof(int));
            Assert.Throws<InvalidOperationException>(() => triggerInfo.ValidateAndThrow());
        }

        /// <summary>
        /// Nulls the trigger type test.
        /// </summary>
        [Fact]
        public void NullTriggerTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((ITriggerInfo)null).ValidateAndThrow());
        }
    }
}
