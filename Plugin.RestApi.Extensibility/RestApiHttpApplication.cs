using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.FilterBindingSyntax;
using Plugin.RestApi.Extensibility.Validation;
using Plugin.Core.Extensibility.Plugins;

namespace Plugin.RestApi.Extensibility
{
    public class RestApiHttpApplication : HttpApplication
    {
        protected void Application_Start()
        {
            PluginsInitializer.Initialize();
            PluginsInitializer.Kernel.BindHttpFilter<ModelValidatorAttribute>(FilterScope.Global);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(PluginsInitializer.Kernel);
            ApplicationStarting();
        }

        protected virtual void ApplicationStarting()
        {
        }
    }
}