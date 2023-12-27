using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubFightRepository : CrudDatabaseRepository<ClubFight, StakeholdersContext>, IClubFightRepository
{
    private DbSet<ClubFight> _dbSet;
    
    public ClubFightRepository(StakeholdersContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.ClubFights;
    }

    public ClubFight GetWithClubs(int fightId)
    {
        return _dbSet.Include(cf => cf.Club1).Include(cf => cf.Club2).FirstOrDefault(cf => cf.Id == fightId);
    }

    public ClubFight GetCurrentFightForOneOfTwoClubs(long clubId1, long clubId2)
    {
        return _dbSet.AsNoTracking().FirstOrDefault(cf => cf.IsInProgress && (cf.Club1Id == clubId1 || cf.Club2Id == clubId1 || cf.Club1Id == clubId2 || cf.Club2Id == clubId2));
    }
}