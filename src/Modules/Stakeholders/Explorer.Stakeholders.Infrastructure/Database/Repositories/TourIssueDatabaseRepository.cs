using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class TourIssueDatabaseRepository : CrudDatabaseRepository<TourIssue, StakeholdersContext>, ITourIssueRepository
{
    private readonly DbSet<TourIssue> _dbSet;

    public TourIssueDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<TourIssue>();
    }

    public PagedResult<TourIssue> GetByUserPaged(int page, int pageSize, int userId)
    {
        var task = _dbSet.Where(t => t.UserId == userId).Include(t => t.Comments).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public PagedResult<TourIssue> GetByTourId(int page, int pageSize, int tourId)
    {
        var task = _dbSet.Where(t => t.TourId == tourId).Include(t => t.Comments).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public PagedResult<TourIssue> GetByTourIssueId(int page, int pageSize, int tourIssueId)
    {
        var task = _dbSet.Where(t => t.Id == tourIssueId).Include(t => t.Comments).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }
}