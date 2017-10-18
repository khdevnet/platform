using System.Collections.Generic;
using Plugin.Authentication.Extensibility.Identities.Dto;

namespace Plugin.Authentication.Extensibility.Identities
{
    public interface IIdentityRepository
    {
        void Add(Identity identity);

        bool Exist(string name, string password);

        UserIdentity GetByToken(string token);

        Identity Get(string name);

        IEnumerable<Identity> Get();
    }
}