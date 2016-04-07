// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerBasedTriggerTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the TimerBasedTriggerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Tests.Triggers
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Tasks;
    using AtomicClock.Triggers;

    using Xunit;

    /// <summary>
    /// The timer based trigger test.
    /// </summary>
    public class TimerBasedTriggerTest
    {
        [Fact]
        public void FirstRunTest()
        {
            //var jobActionOptions = new ActionJobOptions { Action = (d,c) => { return; }, Options = null };
            //var jobInfo = new JobInfo<ActionJob>(jobOptions: jobActionOptions);
            //var triggerContext = new TriggerContext(new CancellationToken(), new CustomizedTaskFactory(), new CustomizedTaskScheduler(1));

            //var trigger = new TimerBasedTrigger((firstRun) => TimeSpan.Zero);
            //trigger.Schedule(jobInfo, triggerContext);
            //Assert.()
        }
    }
}
