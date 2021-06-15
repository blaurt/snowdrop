using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Snowdrop.BL.Services.Projects;
using Snowdrop.DAL.Repositories;
using Snowdrop.Data.Entities;
using Snowdrop.Infrastructure.Dto;
using Xunit;

namespace Snowdrop.BL.Tests.Unit.Services
{
    public sealed class ProjectTests
    {
        private readonly IRepository<Project> _repository = Substitute.For<IRepository<Project>>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        [Fact]
        public async Task CreateProject_NewProject_NoExceptionThrown()
        {
            //Arrange
            const int ownerId = 1;
            var dto = new ProjectDto("Test title", "Some test description", ownerId);
            var projectService = new ProjectService(_repository, _mapper);
            
            //Act
            await projectService.CreateProject(dto);
        }

        [Fact]
        public async Task DeleteProject_NoExceptionThrown()
        {
            //Arrange
            const int projectId = 1;
            var projectService = new ProjectService(_repository, _mapper);
            
            //Act
            await projectService.DeleteProject(projectId);
        }
    }
}