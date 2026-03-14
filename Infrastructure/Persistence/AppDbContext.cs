using API_CobraApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_CobraApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<ExternalProvider> ExternalProviders { get; set; }
        public DbSet<UserExternal> UserExternals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FirstName).IsRequired();
                entity.Property(x => x.LastName).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.LinkedCode).IsRequired();

                entity.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<ExternalProvider>()
                .HasIndex(x => x.ProviderName)
                .IsUnique();

            modelBuilder.Entity<UserExternal>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserExternals)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserExternal>()
                .HasOne(ue => ue.ExternalProvider)
                .WithMany(ep => ep.UserExternals)
                .HasForeignKey(ue => ue.ExternalProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExternalProvider>().HasData(
                new ExternalProvider
                {
                    Id = 1,
                    ProviderName = "Google",
                    IsEnabled = true
                }
            );
        }
    }
}
