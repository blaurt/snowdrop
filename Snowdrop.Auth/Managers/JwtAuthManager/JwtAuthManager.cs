using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Snowdrop.Auth.Managers.TokenStorage;
using Snowdrop.Auth.Models;
using Snowdrop.Auth.Models.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Snowdrop.Auth.Managers.JwtAuthManager
{
    public sealed class JwtAuthManager : IJwtAuthManager
    {
        private readonly JwtConfig _config = default;
        private readonly ITokenStorage _tokenStorage = default;
        private readonly byte[] _secret = default;

        public JwtAuthManager(JwtConfig config, ITokenStorage tokenStorage)
        {
            _config = config;
            _tokenStorage = tokenStorage;
            _secret = Encoding.ASCII.GetBytes(_config.Secret);
        }

        public void RemoveRefreshToken(string userName)
        {
            _tokenStorage.InvalidateToken(userName);
        }

        public async Task<JwtAuthResult> GenerateToken(string userName, IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var shouldAddAudienceClaim =
                string.IsNullOrEmpty(claims?.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                _config.Issuer,
                shouldAddAudienceClaim ? _config.Audience : string.Empty,
                expires: now.AddMilliseconds(_config.AccessTokenExpirationInMs),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = new RefreshToken(userName, string.Empty,
                now.AddMilliseconds(_config.RefreshTokenExpirationInMs));

            await _tokenStorage.RememberToken(refreshToken);
            return new JwtAuthResult(accessToken, refreshToken);
        }

        public async Task<JwtAuthResult> RefreshToken(string refreshToken, string accessToken)
        {
            var now = DateTime.UtcNow;
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invalid token param");
            }

            var userName = principal.Identity?.Name;
            var token = await _tokenStorage.GetToken(refreshToken);
            if (!token.UserName.Equals(userName) || token.ExpiresAt < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token mismatch");
            }

            return await GenerateToken(userName, principal.Claims.ToArray());
        }

        private (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _config.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidateAudience = true,
                        ValidAudience = _config.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30),
                    },
                    out var validatedToken);

            return (principal, validatedToken as JwtSecurityToken);
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}