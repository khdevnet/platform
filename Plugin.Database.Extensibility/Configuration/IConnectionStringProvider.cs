namespace Plugin.Database.Extensibility.Configuration
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }
}