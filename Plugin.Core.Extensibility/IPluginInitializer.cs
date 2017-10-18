namespace Plugin.Core.Extensibility
{
    public interface IPluginInitializer : IDiscoverable
    {
        void Initialize();
    }
}