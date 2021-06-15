namespace Snowdrop.Auth.Models.Configuration
{
    public record JwtConfig(string Secret, string Issuer, string Audience, int AccessTokenExpirationInMs,
        int RefreshTokenExpirationInMs);
}