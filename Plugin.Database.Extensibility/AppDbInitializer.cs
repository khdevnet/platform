using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading;

namespace Plugin.Database.Extensibility
{
    public class AppDbInitializer<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
            bool dbExists = context.Database.Exists();
            string dbName = context.Database.Connection.Database;

            if (!dbExists)
            {
                string connString = context.Database.Connection.ConnectionString;
                var builder = new SqlConnectionStringBuilder(connString)
                {
                    InitialCatalog = "master"
                };
                context.Database.Connection.ConnectionString = builder.ConnectionString;
                context.Database.Connection.Open();
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, $"CREATE DATABASE [{dbName}];");
                context.Database.Connection.Close();
                Thread.Sleep(5000);
                context.Database.Connection.ConnectionString = connString;
                context.Database.Connection.Open();
            }

            context.Database.Connection.Close();
        }
    }
}