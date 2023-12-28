using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Enum;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class TourProgressRepository : CrudDatabaseRepository<TourProgress, ToursContext>, ITourProgressRepository
{
    private readonly DbSet<TourProgress> _dbSet;

    public TourProgressRepository(ToursContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<TourProgress>();
    }

    public TourProgress GetActiveByUser(long userId)
    {
        var tourProgress = _dbSet
            .Include(tp => tp.TouristPosition)
            .Include(tp => tp.Tour)
            .ThenInclude(t => t.Keypoints)
            .FirstOrDefault(tp => tp.TouristPosition.UserId == userId && tp.Status == TourProgressStatus.IN_PROGRESS);
        if (tourProgress == null) throw new KeyNotFoundException("Not found: " + userId);
        return tourProgress;
    }

    public List<TourProgress> GetCompletedByUser(long userId)
    {
        var tourProgress = _dbSet
            .Include(tp => tp.TouristPosition)
            .Include(tp => tp.Tour)
            .ThenInclude(t => t.Keypoints)
            .Where(tp => tp.TouristPosition.UserId == userId && tp.Status == TourProgressStatus.COMPLETED)
            .OrderByDescending(tp => tp.LastActivity)
            .Take(10); 
        return tourProgress.ToList();
    }

    public List<TourProgress> GetActiveTours() {
        return _dbSet.Where(tp => tp.Status == TourProgressStatus.IN_PROGRESS).ToList(); 
    }

    public TourProgress GetByUser(long userId)
    {
        var tourProgress = _dbSet
            .Include(tp => tp.TouristPosition)
            .Include(tp => tp.Tour)
            .ThenInclude(t => t.Keypoints)
            .FirstOrDefault(tp => tp.TouristPosition.UserId == userId);
        if (tourProgress == null) throw new KeyNotFoundException("Not found: " + userId);
        return tourProgress;
    }
}