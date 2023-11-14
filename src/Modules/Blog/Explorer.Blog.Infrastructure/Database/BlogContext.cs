using Explorer.Blog.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<Core.Domain.Blog> Blogs { get; set; }
    public DbSet<BlogComment> ForumComments { get; set; }
    public DbSet<BlogStatus> BlogStatuses { get; set; }
    public DbSet<BlogRating> BlogRatings { get; set; }


    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
        modelBuilder.Entity<BlogRating>().HasNoKey();

        ConfigureBlog(modelBuilder);
    }

    private static void ConfigureBlog(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogStatus>().HasOne<Core.Domain.Blog>().WithMany(b => b.BlogStatuses).HasForeignKey(bs => bs.BlogId);
        modelBuilder.Entity<Core.Domain.Blog>().Property(item => item.BlogRatings).HasColumnType("jsonb");

    }
}