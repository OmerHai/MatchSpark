using MatchSpark.Core.Entities;

namespace MatchSpark.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}