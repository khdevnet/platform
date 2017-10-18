using System;
using Plugin.Authentication.Extensibility.Identities;
using Plugin.Authentication.Extensibility.Tokens;
using Plugin.Authentication.Extensibility.Tokens.Dto;
using Plugin.Core.Extensibility.TimeProviders;

namespace Plugin.Authentication.Service.Tokens
{
    internal class TokenIdentifier : ITokenIdentifier
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly ITokenRepository tokenRepository;
        private readonly IIdentityRepository identityRepository;

        public TokenIdentifier(ITokenRepository tokenRepository, IIdentityRepository identityRepository, ITokenGenerator tokenGenerator)
        {
            this.tokenRepository = tokenRepository;
            this.tokenGenerator = tokenGenerator;
            this.identityRepository = identityRepository;
        }

        public string GenerateToken(string name, string password)
        {
            string tokenValue = tokenRepository.GetTokenByName(name).Value;
            if (!string.IsNullOrEmpty(tokenValue))
            {
                return tokenValue;
            }

            if (identityRepository.Exist(name, password))
            {
                var token = new Token(
                    identityRepository.Get(name).Id,
                    tokenGenerator.Generate(),
                    TimeProvider.Current.Now,
                    GetExpiredTime());

                tokenRepository.Add(token);

                return token.Value;
            }

            return String.Empty;
        }

        public bool ValidateToken(string token)
        {
            if (tokenRepository.Exist(token))
            {
                Token tokenEntity = tokenRepository.GetToken(token);
                if (tokenEntity.ExpiredTime >= TimeProvider.Current.Now)
                {
                    return true;
                }
                RemoveToken(tokenEntity);
            }

            return false;
        }

        private void RemoveToken(Token tokenEntity)
        {
            tokenRepository.Remove(tokenEntity);
        }

        private static DateTime GetExpiredTime()
        {
            return TimeProvider.Current.Now + TimeSpan.FromDays(1);
        }
    }
}