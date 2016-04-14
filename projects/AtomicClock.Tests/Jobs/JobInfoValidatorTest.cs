// <copyright file="JobInfoValidatorTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.Jobs
{
    using System;

    using AtomicClock.Jobs;

    using NSubstitute;

    using Xunit;

    /// <summary>
    /// Tests for JobInfoValidator
    /// </summary>
    public class JobInfoValidatorTest
    {
        /// <summary>
        /// Invalids the job type test.
        /// </summary>
        [Fact]
        public void InvalidJobTypeTest()
        {
            var jobInfo = Substitute.For<IJobInfo>();
            jobInfo.JobType.Returns(_ => typeof(int));
            Assert.Throws<InvalidOperationException>(() => jobInfo.ValidateAndThrow());
        }

        /// <summary>
        /// Nulls the job type test.
        /// </summary>
        [Fact]
        public void NullJobTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((IJobInfo)null).ValidateAndThrow());
        }
    }
}
