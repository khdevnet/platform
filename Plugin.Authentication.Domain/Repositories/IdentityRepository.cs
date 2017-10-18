using System;
using System.Collections.Generic;
using Plugin.Database.Extensibility.Repositories;
using Plugin.Authentication.Domain.Database;
using Plugin.Authentication.Extensibility.Identities;
using Plugin.Authentication.Extensibility.Identities.Dto;

namespace Plugin.Authentication.Domain.Repositories
{
    public class IdentityRepository : RepositoryBase<IDatabase>, IIdentityRepository
    {
        public IdentityRepository(IDatabase context) : base(context)
        {
        }

        public void Add(Identity identity)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Identity Get(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Identity> Get()
        {
            throw new NotImplementedException();
        }

        public UserIdentity GetByToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}