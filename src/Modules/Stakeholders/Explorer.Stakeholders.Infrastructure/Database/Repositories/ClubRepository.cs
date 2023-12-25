using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public PagedResult<Club> GetAll(int page, int pageSize)
    {
        var task = DbContext.Clubs
                .Include("Members")
                .Include("Achievements")
                .OrderByDescending(c => c.FightsWon)
                .GetPaged(page, pageSize);
        task.Wait();
        return task.Result;
    }
}