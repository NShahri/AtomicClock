namespace AtomicClock.WinService
{
    using System;

    using AtomicClock.Services;

    public static class LogHelper
    {
        public static NLog.LogLevel ToNLogLevel(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
    }
}
