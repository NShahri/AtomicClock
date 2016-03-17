namespace AtomicClock.WinService.Jobs
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    public class SampleJob :  IJob
    {
        private readonly string data;

        public SampleJob(string data)
        {
            this.data = data;
        }

        public void Execute(JobContext context)
        {
            Console.WriteLine("Started: {0} - {1}", this.data, DateTime.Now);
            Thread.Sleep(5000);
            Console.WriteLine("Done:    {0} - {1}", this.data, DateTime.Now);
        }
    }
}
