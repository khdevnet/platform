using System;
using System.Reflection;
using log4net;
using Plugin.Logging.Extensibility;

namespace Plugin.Logging
{
    public class Logger : ILogger
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Warn(string message, Exception exception)
        {
            Log.Warn(message, exception);
        }
    }
}