using System.Runtime.CompilerServices;
using MatchSpark.Core.Common;
using MatchSpark.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<OperationResult> RegisterUserAsync(AppUser newUser, string password)
        {
            if(await GetUserByEmailAsync(newUser.Email) != null)
                return OperationResult.Fail("User with this email already exists.");

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}