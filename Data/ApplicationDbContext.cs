using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Player { get; set; }
    public DbSet<PlayerHistory> PlayerHistory { get; set; }
    public DbSet<QuestTemplate> QuestTemplates { get; set; }
    public DbSet<QuestAction> QuestActions { get; set; }
    public DbSet<QuestReward> QuestRewards { get; set; }
    public DbSet<DailyQuest> DailyQuests { get; set; }
    public DbSet<QuestActionProgress> QuestActionProgresses { get; set; }
    public DbSet<QuestPunishment> QuestPunishments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasOne(u => u.Player)
            .WithOne(ps => ps.User)
            .HasForeignKey<Player>(ps => ps.UserId)
            .IsRequired();

        builder.Entity<Player>()
            .Property(ps => ps.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<Player>().HasMany(p => p.History)
            .WithOne(ph => ph.Player)
            .HasForeignKey(ph => ph.PlayerId);

        builder.Entity<QuestTemplate>()
            .HasMany(q => q.Actions)
            .WithOne(a => a.QuestTemplate)
            .HasForeignKey(a => a.QuestTemplateId);

        builder.Entity<QuestTemplate>()
            .HasMany(q => q.Rewards)
            .WithOne(r => r.QuestTemplate)
            .HasForeignKey(r => r.QuestTemplateId);

        builder.Entity<QuestTemplate>().HasMany(q => q.Punishments)
            .WithOne(p => p.QuestTemplate)
            .HasForeignKey(p => p.QuestTemplateId);

        builder.Entity<DailyQuest>()
            .HasMany(uq => uq.ActionProgresses)
            .WithOne(ap => ap.DailyQuest)
            .HasForeignKey(ap => ap.DailyQuestId);
    }
}