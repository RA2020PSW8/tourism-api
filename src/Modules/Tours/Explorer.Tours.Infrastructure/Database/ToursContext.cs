using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Object = Explorer.Tours.Core.Domain.Object;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TouristEquipment> TouristEquipment { get; set; }   
    public DbSet<TourReview> TourReviews { get; set; }
    public DbSet<TourPreference> TourPreference { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourIssue> TourIssue { get; set; }
    public DbSet<Keypoint> Keypoints { get; set; }
    public DbSet<Object> Objects { get; set; }
    
    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        ConfigureTour(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        //TODO: Entity connection
    }
}