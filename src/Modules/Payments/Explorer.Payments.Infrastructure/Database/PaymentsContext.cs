using Explorer.Payments.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options)
    {
    }

    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }

    public DbSet<BundlePrice> TourBundles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");
    }
}