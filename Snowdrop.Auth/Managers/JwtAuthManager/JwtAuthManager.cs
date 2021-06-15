using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public async Task<JwtAuthResult> GenerateToken(string userName, Claim[] claims)
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
            _tokenStorage
        }
    }
}