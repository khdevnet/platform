using System;
using log4net.Core;
using ILogger = Plugin.Logging.Extensibility.ILogger;

namespace Plugin.Logging
{
    public class FallbackLoggingErrorHandler : IErrorHandler
    {
        public static ILogger Logger { get; set; }

        public void Error(string message)
        {
            Error(message, null, ErrorCode.GenericFailure);
        }

        public void Error(string message, Exception exception)
        {
            Error(message, exception, ErrorCode.GenericFailure);
        }

        public void Error(string message, Exception exception, ErrorCode errorCode)
        {
            Logger?.Error(message, exception);
        }
    }
}