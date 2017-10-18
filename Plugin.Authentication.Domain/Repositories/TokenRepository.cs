using System.Linq;
using Plugin.Database.Extensibility.Repositories;
using Plugin.Authentication.Domain.Database;
using Plugin.Authentication.Domain.Database.Model;
using Plugin.Authentication.Extensibility.Tokens;
using TokenDto = Plugin.Authentication.Extensibility.Tokens.Dto.Token;

namespace Plugin.Authentication.Domain.Repositories
{
    internal class TokenRepository : RepositoryBase<IDatabase>, ITokenRepository
    {
        public TokenRepository(IDatabase context) : base(context)
        {
        }

        public void Add(TokenDto token)
        {
            Context.Tokens.Add(new Token
            {
                Id = token.Id,
                Value = token.Value,
                ExpiredTime = token.ExpiredTime,
                GenerationTime = token.GenerationTime
            });
        }

        public bool Exist(string token)
        {
            return Context.Tokens.Any(t => t.Value == token);
        }

        public TokenDto GetToken(string token)
        {
            return Context.Tokens.Select(t => new TokenDto(t.Id, t.Value, t.GenerationTime, t.ExpiredTime)).Single(t => t.Value == token);
        }

        public TokenDto GetTokenByName(string name)
        {
            return Context.Tokens.Where(t => t.Identity.Name == name).Select(t => new TokenDto(t.Id, t.Value, t.GenerationTime, t.ExpiredTime)).Single();
        }

        public bool IsTokenExist(string token)
        {
            return Context.Tokens.Any(t => t.Value == token);
        }

        public void Remove(TokenDto token)
        {
            Context.Tokens.Remove(Context.Tokens.Single(t => t.Id == token.Id));
        }
    }
}