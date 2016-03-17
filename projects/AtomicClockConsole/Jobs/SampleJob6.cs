namespace AtomicClock.WinService.Jobs
{
    using System;

    public class SampleJob6
    {
        public static void DoJob(dynamic data)
        {
            Console.WriteLine("Started: {0} - {1}", data, DateTime.Now);
            throw new NotImplementedException("Exception in job");
        }
    }
}
