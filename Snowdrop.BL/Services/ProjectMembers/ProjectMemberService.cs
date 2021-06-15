using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Snowdrop.DAL.Repositories;
using Snowdrop.Data.Entities;

namespace Snowdrop.BL.Services.ProjectMembers
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IRepository<Project> _repository = default;
        private readonly IMapper _mapper = default;

        public ProjectMemberService(IRepository<Project> repository, IMapper mapper)
        {
            (_repository, _mapper) = (repository, mapper);
        }

        public async Task RemoveMember(int projectId, int userId)
        {
            var project = await _repository.GetSingle(projectId);
            var member = project.Team.SingleOrDefault(m => m.UserId == userId);

            project.Team.Remove(member);
            await _repository.Update(project);
        }
    }
}