using System.Net.Http;
using Snowdrop.Data.Enums;

namespace Snowdrop.Data.Entities
{
    public class ProjectMember
    {
        public int UserId { get; set; }
        public int ProjectId  { get; set; }
        public TeamMemberRole Role { get; set; }
        
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }
}