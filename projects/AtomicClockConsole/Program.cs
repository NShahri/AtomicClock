namespace AtomicClock.WinService
{
    using Topshelf;

    internal class Program
    {
        private static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();

                x.SetDescription("AtomicClock");
                x.SetDisplayName("AtomicClock");
                x.SetServiceName("AtomicClock");

                x.Service<SampleServiceControl>(s =>
                    {
                        s.ConstructUsing(() => new SampleServiceControl());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
            });
        }
    }
}
