using System.Threading.Tasks;

namespace Snowdrop.BL.Services.ProjectMembers
{
    public interface IProjectMemberService
    {
        Task RemoveMember(int projectId, int userId);
    }
}