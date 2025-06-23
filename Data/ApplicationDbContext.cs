using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ZeroToHeroAPI.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PlayerStat> PlayerStats { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasOne(u => u.PlayerStat)
            .WithOne(ps => ps.User)
            .HasForeignKey<PlayerStat>(ps => ps.UserId)
            .IsRequired();

        builder.Entity<PlayerStat>()
            .Property(ps => ps.Id)
            .ValueGeneratedOnAdd();
    }
}