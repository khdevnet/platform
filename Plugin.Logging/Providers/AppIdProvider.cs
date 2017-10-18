using System;
using Plugin.Core.Extensibility;
using Plugin.Logging.Extensibility.Providers;

namespace Plugin.Logging.Providers
{
    public class AppIdProvider : IAppIdProvider
    {
        private const string AppIdKey = "ApplicationId";
        private readonly IAppSettingsProvider applicationSettings;

        public AppIdProvider(IAppSettingsProvider applicationSettings)
        {
            this.applicationSettings = applicationSettings;
        }

        public string GetAppId()
        {
            if (applicationSettings.ContainsKey(AppIdKey))
            {
                string value = applicationSettings.GetValue(AppIdKey);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
            return $"{Environment.UserName}@{Environment.MachineName}";
        }
    }
}