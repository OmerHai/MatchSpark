using MatchSpark.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchSpark.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public virtual DbSet<AppUser> Users { get; set; }
}
