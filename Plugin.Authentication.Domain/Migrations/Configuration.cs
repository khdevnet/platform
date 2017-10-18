using Plugin.Database.Extensibility.Migrations;
using Plugin.Authentication.Domain.Database;

namespace Plugin.Authentication.Domain.Migrations
{
    public sealed class Configuration : AppDbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void DoSeed(DatabaseContext context)
        {
        }
    }
}