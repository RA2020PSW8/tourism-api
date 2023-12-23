using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubChallengeRequestRepository : CrudDatabaseRepository<ClubChallengeRequest, StakeholdersContext>, IClubChallengeRequestRepository
{
    private readonly DbSet<ClubChallengeRequest> _dbSet;

    public ClubChallengeRequestRepository(StakeholdersContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<ClubChallengeRequest>();
    }

    public ClubChallengeRequest GetNoTracking(long id)
    {
        return _dbSet.AsNoTracking().FirstOrDefault(r => r.Id == id);
    }

    public ClubChallengeRequest GetByIdWithClubs(long id)
    {
        return _dbSet.AsNoTracking()
            .Include(cf => cf.Challenger)
            .Include(cf => cf.Challenged)
            .FirstOrDefault(cf => cf.Id == id);
    }
}