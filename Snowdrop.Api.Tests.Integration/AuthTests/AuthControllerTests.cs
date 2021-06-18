using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Snowdrop.Api.Tests.Integration.Base;
using Snowdrop.Api.Tests.Integration.Helpers;
using Snowdrop.Auth.Models;
using Snowdrop.DAL.Repositories;
using Snowdrop.Data.Entities;
using Snowdrop.Infrastructure.Dto.Users;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Snowdrop.Api.Tests.Integration.AuthTests
{
    public sealed class AuthControllerTests : IClassFixture<EnvironmentFixture<Startup>>
    {
        private readonly HttpClient _client = default;
        private readonly IRepository<User> _repository = default;

        public AuthControllerTests(EnvironmentFixture<Startup> fixture)
        {
            _client = fixture.HttpClient;
            _repository = fixture.Services.GetService<IRepository<User>>();
        }

        [Fact, Order(1)]
        public async Task SignUp_ValidUser_SignUpSuccessResponse()
        {
            //Arrange
            const string email = "some.test@email.com";
            const string password = "12345";
            const string route = "auth/signup";
            var request = new SignUpRequest(email, password);

            //Act
            var response = await _client.PostAsync(route, TestProjectHelper.GetStringContent(request));
            var content = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<JwtAuthResult>(content);
            var user = await _repository.GetSingle(1);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(content);
            Assert.NotNull(user);
        }

        [Fact, Order(2)]
        public async Task SignIn_ValidUser_SignUpSuccessResponse()
        {
            //Arrange
            const string email = "some.test@email.com";
            const string password = "12345";
            const string route = "auth/signin";
            var request = new SignInRequest(email, password);

            //Act
            var response = await _client.PostAsync(route, TestProjectHelper.GetStringContent(request));
            var content = await response.Content.ReadAsStringAsync();
            // var authResult = JsonConvert.DeserializeObject<JwtAuthResult>(content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(content);
        }
    }
}