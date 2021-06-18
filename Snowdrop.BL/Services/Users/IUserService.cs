using System.Threading.Tasks;
using Snowdrop.Infrastructure.Dto.Users;

namespace Snowdrop.BL.Services.Users
{
    public interface IUserService
    {
        Task<UserResponse> SingUp(SignUpRequest request);
        Task<UserResponse> SingIn(SignInRequest request);
    }
}