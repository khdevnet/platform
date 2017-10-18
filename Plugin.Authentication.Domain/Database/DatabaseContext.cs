using System.Data.Entity;
using Plugin.Database.Extensibility;
using Plugin.Database.Extensibility.Configuration;
using Plugin.Authentication.Domain.Database.Model;
using DatabaseEntity = System.Data.Entity.Database;

namespace Plugin.Authentication.Domain.Database
{
    public interface IDatabase : IDatabaseBase
    {
        IDbSet<Identity> Identities { get; set; }

        IDbSet<Token> Tokens { get; set; }
    }

    public class DatabaseContext : AppDbContext, IDatabase
    {
        static DatabaseContext()
        {
            DatabaseEntity.SetInitializer(new AppDbInitializer<DatabaseContext>());
        }

        public DatabaseContext(IConnectionStringProvider connectionStringProvider)
            : base(connectionStringProvider)
        {
        }

        public IDbSet<Identity> Identities { get; set; }

        public IDbSet<Token> Tokens { get; set; }
    }
}