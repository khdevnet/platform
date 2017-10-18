using System.Linq;
using System.Web.Configuration;
using Plugin.Core.Extensibility;

namespace Plugin.Web
{
    public class WebConfigApplicationSettings : IAppSettingsProvider
    {
        public string GetValue(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        public bool ContainsKey(string key)
        {
            return WebConfigurationManager.AppSettings.AllKeys.Contains(key);
        }
    }
}