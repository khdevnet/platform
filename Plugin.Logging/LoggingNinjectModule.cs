using Ninject.Modules;
using Plugin.Core;
using Plugin.Core.Extensibility;
using Plugin.Logging.Config;
using Plugin.Logging.Extensibility;
using Plugin.Logging.Extensibility.Providers;
using Plugin.Logging.Providers;

namespace Plugin.Logging
{
    public class LoggingNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<Logger>();
            Bind<ILoggingConfigurationProvider>().To<LoggingConfigurationProvider>();
            Bind<IAppIdProvider>().To<AppIdProvider>();
            Bind<ITempDirectorySettings>().To<TempDirectorySettings>();
        }
    }
}