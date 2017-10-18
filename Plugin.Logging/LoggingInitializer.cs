using System;
using System.IO;
using log4net;
using log4net.Config;
using Plugin.Core.Extensibility;
using Plugin.Logging.Extensibility;
using Plugin.Logging.Extensibility.Providers;

namespace Plugin.Logging
{
    public class LoggingInitializer : IPluginInitializer
    {
        private const string LogDirBasePathSettingName = "Plugin.Logging.LogDirBasePath";
        private readonly ILoggingConfigurationProvider loggingConfigurationProvider;
        private readonly IAppSettingsProvider appSettingsProvider;
        private readonly IAppIdProvider appIdProvider;
        private readonly ITempDirectorySettings tempDirectorySettings;
        private readonly ILogger logger;

        public LoggingInitializer(
            ILoggingConfigurationProvider loggingConfigurationProvider,
            IAppSettingsProvider appSettingsProvider,
            IAppIdProvider appIdProvider,
            ITempDirectorySettings tempDirectorySettings,
            ILogger logger)
        {
            this.loggingConfigurationProvider = loggingConfigurationProvider;
            this.appSettingsProvider = appSettingsProvider;
            this.appIdProvider = appIdProvider;
            this.tempDirectorySettings = tempDirectorySettings;
            this.logger = logger;
        }

        public void Initialize()
        {
            Stream loggingConfig = loggingConfigurationProvider.GetConfig();

            string logDir = GetLogDir();

            GlobalContext.Properties["LogDir"] = logDir;
            GlobalContext.Properties["LogFileName"] = LogFileName(logDir);
            FallbackLoggingErrorHandler.Logger = logger;

            XmlConfigurator.Configure(loggingConfig);
        }

        private string GetLogDirBasePath()
        {
            return appSettingsProvider.ContainsKey(LogDirBasePathSettingName) ? appSettingsProvider.GetValue(LogDirBasePathSettingName) : tempDirectorySettings.TempRootDirectory;
        }

        private string GetLogDir()
        {
            string logDirBasePath = GetLogDirBasePath();

            string applicationName = appIdProvider.GetAppId();

            string logFolderName = $"{applicationName}_Logs";

            string logDir = Path.Combine(logDirBasePath, logFolderName);

            if (!Directory.Exists(logDir))
            {
                try
                {
                    Directory.CreateDirectory(logDir);
                }
                catch
                {
                    throw new DirectoryNotFoundException($"The Directory '{logDir}' does not exists, and could not be created!");
                }
            }
            return logDir;
        }

        private string LogFileName(string logDir)
            => Path.Combine(logDir, $"Logfile_{DateTime.Now.ToString("yyyyMMdd")}.log");
    }
}