using System;

namespace Plugin.Authentication.Extensibility.Tokens.Dto
{
    public class Token
    {
        public Token(int id, string value, DateTime generationTime, DateTime expiredTime)
        {
            Id = id;
            Value = value;
            GenerationTime = generationTime;
            ExpiredTime = expiredTime;
        }

        public int Id { get; }

        public string Value { get; }

        public DateTime GenerationTime { get; }

        public DateTime ExpiredTime { get; }
    }
}