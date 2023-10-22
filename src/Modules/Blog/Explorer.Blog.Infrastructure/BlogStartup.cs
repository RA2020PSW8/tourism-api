using Explorer.Blog.API.Public.Commenting;
using Explorer.Blog.Core.Domain;
<<<<<<< HEAD
using Explorer.Blog.API.Public.Blog;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Mappers;
using Explorer.Blog.Core.UseCases.Blog;
=======
using Explorer.Blog.Core.Mappers;
>>>>>>> 720e58c9b1effce5bfde9cc4a6060d649b514471
using Explorer.Blog.Core.UseCases.Commenting;
using Explorer.Blog.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Blog.Infrastructure;

public static class BlogStartup
{
    public static IServiceCollection ConfigureBlogModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(BlogProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
<<<<<<< HEAD
        services.AddScoped<IBlogService, BlogService>();
=======
>>>>>>> 720e58c9b1effce5bfde9cc4a6060d649b514471
        services.AddScoped<IBlogCommentService,BlogCommentService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
<<<<<<< HEAD
        services.AddScoped(typeof(ICrudRepository<Core.Domain.Blog>), typeof(CrudDatabaseRepository<Core.Domain.Blog, BlogContext>));
=======
>>>>>>> 720e58c9b1effce5bfde9cc4a6060d649b514471
        services.AddScoped(typeof(ICrudRepository<BlogComment>), typeof(CrudDatabaseRepository<BlogComment, BlogContext>));

        services.AddDbContext<BlogContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("blog"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "blog")));
    }
}