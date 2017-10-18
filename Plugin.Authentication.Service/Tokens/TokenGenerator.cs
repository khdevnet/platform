using System;
using Plugin.Authentication.Extensibility.Tokens;

namespace Plugin.Authentication.Service.Tokens
{
    internal class TokenGenerator : ITokenGenerator
    {
        public string Generate()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}