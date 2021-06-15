using System.Threading.Tasks;
using Snowdrop.Infrastructure.Dto;

namespace Snowdrop.BL.Services.Projects
{
    public interface IProjectService
    {
        Task CreateProject(ProjectDto dto);
        
        Task DeleteProject(int projectId);
    }
}