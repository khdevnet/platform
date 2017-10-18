using System;

namespace Plugin.Database.Extensibility
{
    /// <summary>
    /// Base class of DbContext interfaces
    /// </summary>
    public interface IDatabaseBase : IDisposable
    {
        int SaveChanges();
    }
}