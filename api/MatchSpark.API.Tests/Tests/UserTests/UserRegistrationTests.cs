using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using MatchSpark.API.DTOs;
using System.Net;


namespace MatchSpark.API.IntegrationTests.Tests.UserTests
{
    public class UserRegistrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private const string DEFAULT_EMAIL = "test@example.com";
        private const string DEFAULT_PASSWORD = "Test123!";
        private const string DEFAULT_MEDIA_TYPE = "application/json";
        private const string REGISTRATION_END_POINT = "/api/users/register";

        private readonly HttpClient _client;

        public UserRegistrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterUser_ReturnsSuccessStatusCode()
        {
            // Arrange
            UserRegistrationDto newUserDto = new UserRegistrationDto { Email = DEFAULT_EMAIL, Password = DEFAULT_PASSWORD};
            StringContent content = new StringContent(JsonConvert.SerializeObject(newUserDto), Encoding.UTF8, DEFAULT_MEDIA_TYPE);

            // Act
            var response =  await _client.PostAsync(REGISTRATION_END_POINT, content);

            //Assert
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task RegisterUser_EmptyEmailError()
        {
            // Arrange
            UserRegistrationDto newUserDto = new UserRegistrationDto { Password = DEFAULT_PASSWORD };
            StringContent content = new StringContent(JsonConvert.SerializeObject(newUserDto), Encoding.UTF8, DEFAULT_MEDIA_TYPE);

            // Act
            var response = await _client.PostAsync(REGISTRATION_END_POINT, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
            Assert.True(result.ContainsKey("errors"));

            var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(result["errors"].ToString());
            Assert.True(errors.ContainsKey("Email"));

            Assert.Contains("The Email field is required.", errors["Email"]);
        } 

        [Fact]
        public async Task RegisterUser_InvalidEmailError()
        {
            UserRegistrationDto newUserDto = new UserRegistrationDto { Email = "dfasas", Password = DEFAULT_PASSWORD };
            StringContent content = new StringContent(JsonConvert.SerializeObject(newUserDto), Encoding.UTF8, DEFAULT_MEDIA_TYPE);

            var response = await _client.PostAsync(REGISTRATION_END_POINT, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
            Assert.True(result.ContainsKey("errors"));

            var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(result["errors"].ToString());
            Assert.True(errors.ContainsKey("Email"));

            Assert.Contains("The Email field is not a valid e-mail address.", errors["Email"]);
        }

        [Fact]
        public async Task RegisterUser_EmptyPasswordError()
        {
            UserRegistrationDto newUserDto = new UserRegistrationDto { Email = DEFAULT_EMAIL };
            StringContent content = new StringContent(JsonConvert.SerializeObject(newUserDto), Encoding.UTF8, DEFAULT_MEDIA_TYPE);

            var response = await _client.PostAsync(REGISTRATION_END_POINT, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
            Assert.True(result.ContainsKey("errors"));

            var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(result["errors"].ToString());
            Assert.True(errors.ContainsKey("Password"));

            Assert.Contains("The Password field is required.", errors["Password"]);
        }
    }
}