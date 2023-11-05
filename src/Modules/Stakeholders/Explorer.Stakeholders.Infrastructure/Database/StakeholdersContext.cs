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

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) 
    {
       //options.//LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

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
        .WithMany(u => u.Followings);
        /*.UsingEntity(join => join
            .ToTable("UserUsers")
            .MapLeftKey("FollowerUserId")
            .MapRightKey("FollowingUserId"));
        /*.Map(mapping =>
        {
            mapping.ToTable("UserUsers");
            mapping.MapLeftKey("User_Id");
            mapping.MapRightKey("User_Id1");
        });*/
        /*.UsingEntity(u =>
        {
            u.ToTable("UserFollow"); // Define the name of the join table
            u.HasOne<User>().WithMany().HasForeignKey("User_Id");
            u.HasOne<User>().WithMany().HasForeignKey("User_Id1");
        });*/
    }
}