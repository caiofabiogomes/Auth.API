using Auth.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Infrastructure
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }

}
