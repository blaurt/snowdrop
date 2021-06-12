using AutoMapper;
using Snowdrop.Data.Entities;
using Snowdrop.Infrastructure.Dto;

namespace Snowdrop.Infrastructure.MappingProfiles
{
    public sealed class ProjectProfiles: Profile
    {
        public ProjectProfiles()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}