using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snowdrop.Auth.Models;

namespace Snowdrop.Auth.Managers.TokenStorage
{
    public sealed class MemoryTokenStorage : ITokenStorage
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _tokensCollection = default;

        public MemoryTokenStorage()
        {
            _tokensCollection = new ConcurrentDictionary<string, RefreshToken>();
        }

        public Task RememberToken(RefreshToken refreshToken)
        {
            return Task.FromResult(_tokensCollection.AddOrUpdate(refreshToken.Token, refreshToken,
                (_, _) => refreshToken));
        }

        public void InvalidateToken(string userName)
        {
            var tokens = _tokensCollection.Where(p => p.Value.UserName.Equals(userName));

            foreach (var pair in tokens)
            {
                _tokensCollection.TryRemove(pair.Key, out _);
            }
        }

        public Task<RefreshToken> GetToken(string refreshToken)
        {
            _tokensCollection.TryGetValue(refreshToken, out var token);
            return Task.FromResult<RefreshToken>(token);
        }
    }
}