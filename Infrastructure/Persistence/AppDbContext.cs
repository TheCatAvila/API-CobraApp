using API_CobraApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_CobraApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FirstName).IsRequired();
                entity.Property(x => x.LastName).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.LinkedCode).IsRequired();

                entity.HasIndex(x => x.Email).IsUnique();
            });
        }
    }
}
