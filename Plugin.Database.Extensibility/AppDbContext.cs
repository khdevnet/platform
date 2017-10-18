using System.Data.Entity;
using Plugin.Database.Extensibility.Configuration;
using Plugin.Database.Extensibility.Migrations;

namespace Plugin.Database.Extensibility
{
    [DbConfigurationType(typeof(AppDbConfiguration))]
    public abstract class AppDbContext : DbContext, IDatabaseBase, IAppDbContext
    {
        protected AppDbContext(
            IConnectionStringProvider connectionStringProvider)
            : base(connectionStringProvider.GetConnectionString())
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}