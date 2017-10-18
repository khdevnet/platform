using Ninject.Modules;
using Plugin.Http.Extensibility.Senders;
using Plugin.Http.Senders;

namespace Plugin.Http
{
    public class DatabaseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpMessageSender>().To<HttpMessageSender>();
        }
    }
}