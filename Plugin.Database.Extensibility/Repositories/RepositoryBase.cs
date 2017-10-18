namespace Plugin.Database.Extensibility.Repositories
{
    public abstract class RepositoryBase<TDbContext> where TDbContext : IDatabaseBase
    {
        protected readonly TDbContext Context;

        protected RepositoryBase(TDbContext context)
        {
            Context = context;
        }
    }
}