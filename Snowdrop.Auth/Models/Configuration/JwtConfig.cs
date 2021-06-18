namespace Snowdrop.Auth.Models.Configuration
{
    // public record JwtConfig(string Secret, string Issuer, string Audience, int AccessTokenExpirationInMs,
    //     int RefreshTokenExpirationInMs);
    
    public sealed class JwtConfig
    {
        public string Secret { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int AccessTokenExpirationInMs { get; init; }
        public int RefreshTokenExpirationInMs { get; init; }

        public JwtConfig()
        {
        }
    }
}