using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubRepository : CrudDatabaseRepository<Club, StakeholdersContext>, IClubRepository
{
    private readonly DbSet<Club> _dbSet;
    
    public ClubRepository(StakeholdersContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Clubs;
    }

    public Club GetUntracked(long id)
    {
        return _dbSet.AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public Club GetWithMembers(long id)
    {
        return _dbSet.AsNoTracking().Include(c => c.Owner).Include(c => c.Members).FirstOrDefault(c => c.Id == id);
    }
}