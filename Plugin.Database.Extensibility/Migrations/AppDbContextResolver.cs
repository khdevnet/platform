using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using Ninject;
using Ninject.Syntax;
using Plugin.Database.Extensibility.Configuration;

namespace Plugin.Database.Extensibility.Migrations
{
    public class AppDbContextResolver : IDbDependencyResolver
    {
        public static IResolutionRoot Kernel { private get; set; }

        public object GetService(Type type, object key)
        {
            if (type.IsAssignableFrom(typeof(Func<DbContext>)))
            {
                var targetDatabaseType = key as Type;
                if (typeof(AppDbContext).IsAssignableFrom(targetDatabaseType))
                {
                    if (Kernel != null)
                    {
                        Func<DbContext> databaseFactory = () => (DbContext)Kernel.Get(targetDatabaseType);
                        return databaseFactory;
                    }

                    return new Func<DbContext>(
                        () => targetDatabaseType
                        .GetConstructor(new[] { typeof(IConnectionStringProvider) })
                        .Invoke(new object[] { null }) as DbContext);
                }
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type type, object key)
        {
            object service = GetService(type, key);
            return service == null ? Enumerable.Empty<object>() : new[] { service };
        }
    }
}