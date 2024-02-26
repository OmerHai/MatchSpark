namespace MatchSpark.Core.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public AppUser(string email, string passwordHash, string passwordSalt)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));  
            PasswordSalt = passwordSalt ?? throw new ArgumentNullException(nameof(passwordSalt));      
        }
    }
}