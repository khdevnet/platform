using System.IO;
using Plugin.Logging.Extensibility;
using Plugin.Logging.Properties;

namespace Plugin.Logging.Config
{
    public class LoggingConfigurationProvider : ILoggingConfigurationProvider
    {
        public Stream GetConfig()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(Resources.Log4Net);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}