using Plugin.Core.Extensibility;

namespace Plugin.Database.Extensibility
{
    public interface IDatabaseModuleInitializer : IDiscoverable
    {
        void Initialize();
    }
}