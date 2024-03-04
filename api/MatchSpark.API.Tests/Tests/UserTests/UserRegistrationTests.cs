using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using MatchSpark.API.DTOs; 

namespace MatchSpark.API.IntegrationTests.Tests.UserTests
{
    public class UserRegistrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserRegistrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_RegisterUser_ReturnsSuccessStatusCode()
        {
            // Arrange
            UserRegistrationDto newUserDto = new UserRegistrationDto { Email = "test@example.com", Password = "Test123!" };
            StringContent content = new StringContent(JsonConvert.SerializeObject(newUserDto), Encoding.UTF8, "application/json");

            // Act
            var response =  await _client.PostAsync("/api/users/register", content);

            //Assert
            response.EnsureSuccessStatusCode();

        }

    }
}