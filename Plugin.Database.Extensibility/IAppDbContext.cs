using Plugin.Core.Extensibility;

namespace Plugin.Database.Extensibility
{
    /// <summary>
    /// Interface to help finding PluginDbContext subclasses
    /// </summary>
    public interface IAppDbContext : IDiscoverable
    {
    }
}