using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using MatchSpark.Core.Entities;
using MatchSpark.Data;
using MatchSpark.Data.Services;

namespace MatchSpark.Core.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task RegisterUserAsync_CreatesNewUser()
        {
            // Arrange
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb");
            
            var options = optionsBuilder.Options;

            using(ApplicationDbContext context = new ApplicationDbContext(options))
            {
                UserServices service = new UserServices(context); 
                AppUser user = new AppUser { Email = "test@example.com" };
               
                // Act
                bool result = await service.RegisterUserAsync(user, "TestPassword123!");
                Assert.True(result);
            }
        }
    }
}
