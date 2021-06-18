using System.Data;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Snowdrop.DAL.Repositories;
using Snowdrop.Data.Entities;
using Snowdrop.Infrastructure.Dto.Users;

namespace Snowdrop.BL.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        // TODO get from config
        private const string Salt = "bm12b31jy23gb1bvenv1b2v3bnv1b2n3";

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserResponse> SingUp(SignUpRequest request)
        {
            var existingUser = await _repository.GetSingle(u => u.Email == request.UserName.ToLowerInvariant());

            if (existingUser != null)
            {
                throw new DuplicateNameException($"User with username \"{request.UserName}\" already exists");
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = HashHelper.Create(request.Password, Salt);
            await _repository.Insert(user);
            var claims = new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            return new UserResponse(user.Email, claims);
        }

        public async Task<UserResponse> SingIn(SignInRequest request)
        {
            var user = await _repository.GetSingle(u => u.Email == request.UserName.ToLowerInvariant());
            if (user == null)
            {
                throw new AuthenticationException("Incorrect password or login");
            }


            if (!HashHelper.Validate(request.Password, Salt, user.PasswordHash))
            {
                throw new AuthenticationException("Incorrect password or login");
            }

            var claims = new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            return new UserResponse(user.Email, claims);
        }
    }
}