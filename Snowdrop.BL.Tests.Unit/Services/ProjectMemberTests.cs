using System;
using System.Collections.Generic;
using AutoMapper;
using NSubstitute;
using Snowdrop.BL.Services.ProjectMembers;
using Snowdrop.DAL.Repositories;
using Snowdrop.Data.Entities;
using Xunit;

namespace Snowdrop.BL.Tests.Unit.Services
{
    public class ProjectMemberTests
    {
        private ProjectMemberService _service = default;
        private IRepository<Project> _repository = default;
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        [Fact]
        public async void RemoveMember_ProjectExists_NoExceptionThrown()
        {
            //Arrange
            const int projectId = 1;
            const int userId = 1;
            var mockProject = new Project()
            {
                Id = projectId,
                Team = new List<ProjectMember>()
                {
                    new ProjectMember()
                    {
                        ProjectId = projectId, UserId = userId
                    }
                }
            };
            _repository = Substitute.For<IRepository<Project>>();
            _repository.GetSingle(projectId).Returns(mockProject);
            _service = new ProjectMemberService(_repository, _mapper);
            
            //Act
            await _service.RemoveMember(projectId, userId);

            //Assert
            
        }
    }
}