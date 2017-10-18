using System;
using System.Configuration;
using Plugin.Database.Extensibility.Configuration;

namespace Plugin.Database.Configuration
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        protected const string DefaultDatabaseName = "SiteDbContext";

        public string GetConnectionString()
        {
            ConnectionStringSettings configuration = ConfigurationManager.ConnectionStrings[DefaultDatabaseName];

            if (!String.IsNullOrEmpty(configuration?.ConnectionString))
            {
                return configuration.ConnectionString;
            }

            throw new ArgumentNullException(nameof(ConfigurationManager.ConnectionStrings), $"Add database connection with name {DefaultDatabaseName}");
        }
    }
}