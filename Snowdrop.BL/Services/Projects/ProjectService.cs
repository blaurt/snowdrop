using System.Threading.Tasks;
using AutoMapper;
using Snowdrop.DAL.Repositories;
using Snowdrop.Data.Entities;
using Snowdrop.Infrastructure.Dto;

namespace Snowdrop.BL.Services.Projects
{
    public sealed class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository = default;
        private readonly IMapper _mapper = default;

        public ProjectService(IRepository<Project> repository, IMapper mapper)
        {
            (_repository, _mapper) = (repository, mapper);
        }

        public async Task CreateProject(ProjectDto dto)
        {
            await _repository.Insert(_mapper.Map<Project>(dto));
        }

        public async Task DeleteProject(int projectId)
        {
            await _repository.Delete(projectId);
        }
    }
}