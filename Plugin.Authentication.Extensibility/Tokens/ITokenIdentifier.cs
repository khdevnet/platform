namespace Plugin.Authentication.Extensibility.Tokens
{
    public interface ITokenIdentifier
    {
        string GenerateToken(string email, string password);

        bool ValidateToken(string token);
    }
}