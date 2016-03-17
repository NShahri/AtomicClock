namespace AtomicClock.Services
{
    using System;

    internal class LoggerService
    {
        private readonly string loggerName;

        public LoggerService(string loggerName)
        {
            this.loggerName = loggerName;
        }

        public void Error(string message, Exception ex)
        {
            LogManager.Log(LogLevel.Error, this.loggerName, message, ex);    
        }

        public void Fatal(string message, Exception ex)
        {
            LogManager.Log(LogLevel.Fatal, this.loggerName, message, ex);
        }

        public void Debug(string message)
        {
            LogManager.Log(LogLevel.Debug, this.loggerName, message, null);
        }

        public void Info(string message)
        {
            LogManager.Log(LogLevel.Info, this.loggerName, message, null);
        }
    }
}
