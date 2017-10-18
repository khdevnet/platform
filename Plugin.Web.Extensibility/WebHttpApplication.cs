using System.Web;
using Plugin.Core.Extensibility.Plugins;

namespace Plugin.Web.Extensibility
{
    public class WebHttpApplication : HttpApplication
    {
        protected void Application_Start()
        {
            PluginsInitializer.Initialize();
            OnApplicationStarting();
        }

        protected virtual void OnApplicationStarting()
        {
        }
    }
}