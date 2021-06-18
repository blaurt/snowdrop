using Microsoft.Extensions.DependencyInjection;
using Snowdrop.BL.Services.ProjectMembers;
using Snowdrop.BL.Services.Projects;
using Snowdrop.BL.Services.Users;

namespace Snowdrop.BL.Tests.Unit.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnowdropServices(this IServiceCollection collection)
        {
            collection.AddScoped<IProjectService, ProjectService>();
            collection.AddScoped<IProjectMemberService, ProjectMemberService>();
            collection.AddScoped<IUserService, UserService>();
            
        }
    }
}