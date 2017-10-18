using System.IO;

namespace Plugin.Logging.Extensibility
{
    public interface ILoggingConfigurationProvider
    {
        Stream GetConfig();
    }
}