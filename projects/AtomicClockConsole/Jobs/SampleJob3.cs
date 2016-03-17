namespace AtomicClock.WinService.Jobs
{
    using System;

    using AtomicClock.Contexts;
    using AtomicClock.Schedulers;
    using AtomicClock.Triggers;

    public class SampleJob3
    {
        public static void DoJob(dynamic data, JobContext context)
        {
            Console.WriteLine("Started: {0} - {1}", data, DateTime.Now);
            context.JobScheduler.ScheduleJob<RunNowTrigger>(SampleJob4.DoJob);
            Console.WriteLine("Done:    {0} - {1}", data, DateTime.Now);
        }
    }
}
