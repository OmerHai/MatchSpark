using MatchSpark.Core.Entities;
using MatchSpark.Core.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace MatchSpark.Core.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly TokenService _tokenService;
        private readonly Mock<IConfiguration> _configMock;

        public TokenServiceTests()
        {
            _configMock = new Mock<IConfiguration>();
            var jwtSectionMock = new Mock<IConfigurationSection>();
            jwtSectionMock.Setup(s => s["Secret"]).Returns("MyVerySecretKeyHerefadsffffffffffffadsfdsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssewafewfewfqqgqrrfwhtejjyryjry");
            _configMock.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSectionMock.Object);
            _tokenService = new TokenService(_configMock.Object);
        }

        [Fact]
        public void CreateToken_ReturnsNonNullTokenForValidUser()
        {
            // Arrange
            var user = new AppUser { Email = "test@example.com", PasswordHash = "123456" };

            // Act
            var token = _tokenService.CreateToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}