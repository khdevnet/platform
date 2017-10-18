using Ninject.Modules;
using Plugin.Authentication.Extensibility.Tokens;
using Plugin.Authentication.Service.Tokens;

namespace Plugin.Authentication.Service
{
    public class AuthenticationNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITokenIdentifier>().To<TokenIdentifier>();
            Bind<ITokenGenerator>().To<TokenGenerator>();
        }
    }
}