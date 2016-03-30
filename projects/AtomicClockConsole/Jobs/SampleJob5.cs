// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleJob5.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   AssemblyInfo.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace AtomicClock.WinService.Jobs
{
    using System;
    using System.Threading;

    public class SampleJob5
    {
        public static void DoJob(dynamic data)
        {
            Console.WriteLine("Started: {0} - {1}", data, DateTime.Now);
            Thread.Sleep(2000);
            Console.WriteLine("Done:    {0} - {1}", data, DateTime.Now);
        }
    }
}