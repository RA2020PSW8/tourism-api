using Explorer.Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<ForumComment> ForumComments { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
    }
}