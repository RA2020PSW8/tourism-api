using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubFightRepository : CrudDatabaseRepository<ClubFight, StakeholdersContext>, IClubFightRepository
{
    public ClubFightRepository(StakeholdersContext dbContext) : base(dbContext)
    {
    }
}