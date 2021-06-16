using System.Linq.Expressions;
using System.Threading.Tasks;
using Snowdrop.Auth.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Snowdrop.Auth.Managers.TokenStorage
{
    public sealed class RedisTokenStorage : ITokenStorage
    {
        private readonly IRedisCacheClient _redisCacheClient = default;

        public Task RememberToken(RefreshToken refreshToken)
        {
            return _redisCacheClient.Db0.AddAsync(refreshToken.UserName, refreshToken, refreshToken.ExpiresAt);
        }

        public void InvalidateToken(string userName)
        {
            _redisCacheClient.Db0.RemoveAsync(userName);
        }

        public Task<RefreshToken> GetToken(string userName)
        {
            return _redisCacheClient.Db0.GetAsync<RefreshToken>(userName);
        }
    }
} 