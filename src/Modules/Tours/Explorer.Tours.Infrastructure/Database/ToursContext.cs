using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Object = Explorer.Tours.Core.Domain.Object;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Keypoint> Keypoints { get; set; }
    public DbSet<Object> Objects { get; set; }
    
    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
    }
}