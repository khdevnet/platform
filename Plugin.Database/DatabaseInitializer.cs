using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Ninject.Syntax;
using Plugin.Database.Extensibility;
using Plugin.Database.Extensibility.Migrations;
using Plugin.Core.Extensibility;

namespace Plugin.Database
{
    public class DatabaseInitializer : IPluginInitializer
    {
        private readonly IAppDbContext[] databaseContexts;
        private readonly IMigrationsConfiguration[] migrationConfigs;
        private readonly IResolutionRoot kernel;

        public DatabaseInitializer(
            IAppDbContext[] databaseContexts,
            IMigrationsConfiguration[] migrationConfigs,
            IResolutionRoot kernel)
        {
            this.databaseContexts = databaseContexts;
            this.migrationConfigs = migrationConfigs;
            this.kernel = kernel;
        }

        public void Initialize()
        {
            AppDbContextResolver.Kernel = kernel;
            foreach (DbContext dbContext in databaseContexts)
            {
                Console.WriteLine("Deploying database: {0}", dbContext.GetType().FullName);
                dbContext.Database.Initialize(false);
            }

            SortedDictionary<string, IMigrationsConfiguration> pendingMigrations = GetPendingMigrations();

            RunMigrations(pendingMigrations);
        }

        private SortedDictionary<string, IMigrationsConfiguration> GetPendingMigrations()
        {
            var result = new SortedDictionary<string, IMigrationsConfiguration>();

            foreach (IMigrationsConfiguration migrationConfig in migrationConfigs)
            {
                DbMigrationsConfiguration configuration = migrationConfig.GetConfiguration();
                Console.WriteLine("Processing migration config: " + configuration.GetType().FullName);
                var migrator = new DbMigrator(configuration);
                IEnumerable<string> pendingMigrations = migrator.GetPendingMigrations().ToArray();

                migrationConfig.IsUpToDate = !pendingMigrations.Any();

                foreach (string migration in pendingMigrations)
                {
                    result[migration] = migrationConfig;
                }
            }

            return result;
        }

        private static void RunMigrations(SortedDictionary<string, IMigrationsConfiguration> migrations)
        {
            if (migrations.Any())
            {
                Console.WriteLine("Applying pending migrations ({0})", migrations.Count);

                foreach (KeyValuePair<string, IMigrationsConfiguration> migrationEntry in migrations)
                {
                    string migration = migrationEntry.Key;

                    IMigrationsConfiguration migrationConfig = migrationEntry.Value;

                    string migrationConfigName = migrationConfig.GetType().FullName;

                    var migrator = new DbMigrator(migrationConfig.GetConfiguration());

                    Console.WriteLine("Applying migration (name: {0}, configuration: {1})", migration, migrationConfigName);

                    try
                    {
                        migrator.Update(migration);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error during migration {0}: \r\n{1}", migration, e);
                        throw;
                    }
                }

                migrations.Values.ToList().ForEach(mc => mc.IsUpToDate = true);
            }
        }
    }
}