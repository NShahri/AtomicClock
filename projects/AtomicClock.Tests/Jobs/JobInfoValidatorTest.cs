// <copyright file="JobInfoValidatorTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.Jobs
{
    using System;

    using AtomicClock.Jobs;

    using NSubstitute;

    using Xunit;

    public class JobInfoValidatorTest
    {
        [Fact]
        public void InvalidJobTypeTest()
        {
            var jobInfo = Substitute.For<IJobInfo>();
            jobInfo.JobType.Returns(_ => typeof(int));
            Assert.Throws<InvalidOperationException>(() => jobInfo.ValidateAndThrow());
        }

        [Fact]
        public void NullJobTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((IJobInfo)null).ValidateAndThrow());
        }
    }
}
