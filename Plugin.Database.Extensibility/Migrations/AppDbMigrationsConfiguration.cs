using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace Plugin.Database.Extensibility.Migrations
{
    public abstract class AppDbMigrationsConfiguration<TDatabaseContext> : DbMigrationsConfiguration<TDatabaseContext>, IMigrationsConfiguration
        where TDatabaseContext : DbContext
    {
        protected AppDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        public bool IsUpToDate { get; set; }

        public virtual IEnumerable<string> DependentSeeds => new string[0];

        public virtual bool ShouldSeed => true;

        public DbMigrationsConfiguration GetConfiguration()
        {
            return this;
        }

        protected override void Seed(TDatabaseContext context)
        {
            if (!IsUpToDate)
            {
                return;
            }

            Debug.WriteLine("Seeding: " + GetType().FullName);
            DoSeed(context);
        }

        protected abstract void DoSeed(TDatabaseContext context);
    }
}