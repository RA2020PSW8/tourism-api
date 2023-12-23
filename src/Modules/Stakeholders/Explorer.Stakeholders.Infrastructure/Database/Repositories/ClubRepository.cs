using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubRepository : CrudDatabaseRepository<Club, StakeholdersContext>, IClubRepository
{
    public ClubRepository(StakeholdersContext dbContext) : base(dbContext)
    {
    }

    public Club GetUntracked(long id)
    {
        return DbContext.Clubs.AsNoTracking().FirstOrDefault(c => c.Id == id);
    }
}