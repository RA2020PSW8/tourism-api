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
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<PaymentRecord> PaymentRecords { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<TourSale> TourSales { get; set; }
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        modelBuilder.Entity<TourSale>()
            .HasKey(ts => new { ts.SaleId, ts.TourId });

        modelBuilder.Entity<TourSale>()
            .HasOne<Sale>()
            .WithMany(e => e.TourSales)
            .HasForeignKey(te => te.SaleId);

        modelBuilder.Entity<Coupon>()
            .Property(e => e.ExpiryDate)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue).Date,
                v => DateOnly.FromDateTime(v.Date))
            .HasColumnType("date");

    }
}