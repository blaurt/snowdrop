using AutoMapper;
using Snowdrop.Data.Entities;
using Snowdrop.Infrastructure.Dto;

namespace Snowdrop.Infrastructure.MappingProfiles
{
    public sealed class ProjectMemberProfile: Profile
    {
        public ProjectMemberProfile()
        {
            CreateMap<ProjectMember, ProjectMemberDto>();
            CreateMap<ProjectMemberDto, ProjectMember>();
        }
    }
}