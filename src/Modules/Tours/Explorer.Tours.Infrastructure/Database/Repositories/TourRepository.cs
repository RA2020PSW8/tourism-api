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

    public PagedResult<Tour> GetByAuthorPaged(int page, int pageSize, int authorId)
    {
        var task = _dbSet.Where(t => t.UserId == authorId).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public PagedResult<Tour> GetPublishedPaged(int page, int pageSize)
    {
        var task = _dbSet.Where(t => t.Status == TourStatus.PUBLISHED).Include(t => t.Keypoints).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }
    public Tour GetById(int id)
    {
        var tour = _dbSet.AsNoTracking().FirstOrDefault(tp => tp.Id == id);
        return tour;
    }
}