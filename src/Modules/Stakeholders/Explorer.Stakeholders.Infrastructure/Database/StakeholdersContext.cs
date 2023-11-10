using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<ApplicationRating> ApplicationRatings { get; set; }
    public DbSet<ClubJoinRequest> ClubJoinRequests { get; set; }
    public DbSet<TourIssue> TourIssue { get; set; }
    public DbSet<ClubInvitation> ClubInvitations { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options)  { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<TourIssue>().HasIndex(t => t.UserId).IsUnique(false);
        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

        modelBuilder.Entity<Person>()
            .HasMany(u => u.Followers)
            .WithMany(u => u.Following);

        modelBuilder.Entity<TourIssueComment>().HasOne<TourIssue>().WithMany(t => t.Comments).HasForeignKey(te => te.TourIssueId);
        modelBuilder.Entity<TourIssueComment>().HasOne<User>().WithMany(u => u.IssueComments).HasForeignKey(t => t.UserId);
        modelBuilder.Entity<TourIssue>().HasOne<User>().WithMany(u => u.Issues).HasForeignKey(t => t.UserId);

    }
}