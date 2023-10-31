using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Enum;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class TourRepository : CrudDatabaseRepository<Tour, ToursContext>, ITourRepository
{
    private readonly DbSet<Tour> _dbSet;
    
    public TourRepository(ToursContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<Tour>();
    }

    public PagedResult<Tour> GetPublishedPaged(int page, int pageSize)
    {
        var task = _dbSet.Where(t => t.Status == TourStatus.PUBLISHED).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }
}