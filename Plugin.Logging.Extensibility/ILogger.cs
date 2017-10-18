using System;

namespace Plugin.Logging.Extensibility
{
    public interface ILogger
    {
        void Info(string message);

        void Warn(string message, Exception exception);

        void Error(string message, Exception exception);
    }
}