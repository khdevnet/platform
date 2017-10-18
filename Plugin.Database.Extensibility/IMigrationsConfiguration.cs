using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Plugin.Core.Extensibility;

namespace Plugin.Database.Extensibility
{
    public interface IMigrationsConfiguration : IDiscoverable
    {
        bool IsUpToDate { get; set; }

        IEnumerable<string> DependentSeeds { get; }

        bool ShouldSeed { get; }

        int? CommandTimeout { get; set; }

        DbMigrationsConfiguration GetConfiguration();
    }
}