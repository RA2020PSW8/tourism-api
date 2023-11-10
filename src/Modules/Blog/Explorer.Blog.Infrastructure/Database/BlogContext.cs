using Explorer.Blog.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<Core.Domain.Blog> Blogs { get; set; }
    public DbSet<BlogComment> ForumComments { get; set; }
    public DbSet<BlogStatus> BlogStatuses { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
        ConfigureBlog(modelBuilder);
    }

    private static void ConfigureBlog(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogStatus>().HasOne<Core.Domain.Blog>().WithMany(b => b.BlogStatuses).HasForeignKey(bs => bs.BlogId);
    }
}