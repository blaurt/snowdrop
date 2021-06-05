using System.Collections.Generic;
using Snowdrop.Data.Entities.Core;

namespace Snowdrop.Data.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public virtual IReadOnlyCollection<Project> OwnProjects { get; set; }
        public virtual IReadOnlyCollection<ProjectMember> Projects { get; set; }
        public virtual IReadOnlyCollection<Balance> Balances { get; set; }
    }
}