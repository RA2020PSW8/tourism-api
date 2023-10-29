using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Core.UseCases.Tourist;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Explorer.Stakeholders.API.Public.TourExecution;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.UseCases.TourExecution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();        
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IClubJoinRequestService, ClubJoinRequestService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IClubInvitationService, ClubInvitationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IApplicationRatingService, ApplicationRatingService>();
        services.AddScoped<ITourIssueService, TourIssueService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ClubJoinRequest>), typeof(CrudDatabaseRepository<ClubJoinRequest, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(IClubJoinRequestRepository), typeof(ClubJoinRequestDatabaseRepository));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<ClubInvitation>), typeof(CrudDatabaseRepository<ClubInvitation, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ApplicationRating>), typeof(CrudDatabaseRepository<ApplicationRating, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<TourIssue>), typeof(CrudDatabaseRepository<TourIssue, StakeholdersContext>));

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}