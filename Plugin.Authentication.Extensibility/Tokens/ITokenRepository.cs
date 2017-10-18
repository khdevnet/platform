using Plugin.Authentication.Extensibility.Tokens.Dto;

namespace Plugin.Authentication.Extensibility.Tokens
{
    public interface ITokenRepository
    {
        void Add(Token token);

        Token GetToken(string token);

        bool Exist(string token);

        void Remove(Token token);

        Token GetTokenByName(string name);
    }
}