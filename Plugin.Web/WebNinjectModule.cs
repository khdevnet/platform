using Ninject.Modules;
using Plugin.Core.Extensibility;

namespace Plugin.Web
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAppSettingsProvider>().To<WebConfigApplicationSettings>();
        }
    }
}