using MatchSpark.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace MatchSpark.Data.Services
{
    public class UserServices
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<AppUser> _passwordHasher;

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<AppUser>();
        }

        public async Task<bool> RegisterUserAsync(AppUser newUser, string password)
        {
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}