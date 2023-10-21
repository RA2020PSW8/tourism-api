using Explorer.Blog.Infrastructure;
using Explorer.Stakeholders.Infrastructure;
using Explorer.Tours.Infrastructure;

namespace Explorer.API.Startup;

public static class ModulesConfiguration
{
    public static IServiceCollection RegisterModules(this IServiceCollection services)
    {
        services.ConfigureStakeholdersModule();
        services.ConfigureToursModule();
        services.ConfigureBlogModule();
<<<<<<< HEAD

=======
>>>>>>> 2dc6df9cd896eac0e2396e42bb8c45552d69f5ad
        return services;
    }
}