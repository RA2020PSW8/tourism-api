using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Object = Explorer.Tours.Core.Domain.Object;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TouristEquipment> TouristEquipment { get; set; }   
    public DbSet<TourReview> TourReviews { get; set; }
    public DbSet<TourPreference> TourPreference { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Keypoint> Keypoints { get; set; }
    public DbSet<Object> Objects { get; set; }
    public DbSet<TourEquipment> TourEquipments { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        ConfigureTour(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Keypoint>()
            .HasOne(k => k.Tour)
            .WithMany(t => t.Keypoints)
            .HasForeignKey(k => k.TourId);
        
        modelBuilder.Entity<TourEquipment>().HasKey(te => new { te.TourId, te.EquipmentId });
        modelBuilder.Entity<TourEquipment>().HasOne<Tour>().WithMany(t => t.TourEquipments).HasForeignKey(te => te.TourId);
        modelBuilder.Entity<TourEquipment>().HasOne<Equipment>().WithMany(e => e.TourEquipments).HasForeignKey(te => te.EquipmentId);
    }
}
