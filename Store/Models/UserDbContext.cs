using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }

    }
}
