using System.Threading.Tasks;
using Snowdrop.Auth.Models;

namespace Snowdrop.Auth.Managers.TokenStorage
{
    public sealed class RedisTokenStorage: ITokenStorage
    {
        public Task RememberToken(RefreshToken refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public void InvalidateToken(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<RefreshToken> GetToken(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}