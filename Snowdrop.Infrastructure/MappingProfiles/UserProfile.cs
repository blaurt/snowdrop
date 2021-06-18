using AutoMapper;
using Snowdrop.Data.Entities;
using Snowdrop.Infrastructure.Dto.Users;

namespace Snowdrop.Infrastructure.MappingProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpRequest, User>()
                .ForMember(
                    u => u.PasswordHash,
                    opt => opt.Ignore()
                )
                .ForMember(
                    u => u.Email,
                    opt => opt.MapFrom(
                        src => src.UserName.ToLowerInvariant()
                    ));
        }
    }
}