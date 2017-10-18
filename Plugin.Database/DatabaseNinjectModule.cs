using Ninject.Modules;
using Plugin.Database.Configuration;
using Plugin.Database.Extensibility.Configuration;

namespace Plugin.Database
{
    public class DatabaseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConnectionStringProvider>().To<ConnectionStringProvider>();
        }
    }
}