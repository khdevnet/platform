using System.Data.Entity;

namespace Plugin.Database.Extensibility.Migrations
{
    public class AppDbConfiguration : DbConfiguration
    {
        public AppDbConfiguration()
        {
            AddDependencyResolver(new AppDbContextResolver());
        }
    }
}