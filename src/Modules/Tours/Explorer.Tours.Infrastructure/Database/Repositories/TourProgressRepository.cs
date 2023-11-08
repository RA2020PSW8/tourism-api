using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Enum;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourProgressRepository : CrudDatabaseRepository<TourProgress, ToursContext>, ITourProgressRepository
    {
        private readonly DbSet<TourProgress> _dbSet;

        public TourProgressRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TourProgress>();
        }

        public TourProgress GetActiveByUser(long userId)
        {
            var tourProgress = _dbSet.Include(tp => tp.TouristPosition).Include(tp => tp.Tour).ThenInclude(t => t.Keypoints).FirstOrDefault(tp => tp.TouristPosition.UserId == userId && tp.Status == TourExecutionStatus.IN_PROGRESS);
            if (tourProgress == null) throw new KeyNotFoundException("Not found: " + userId);
            return tourProgress;
        }
    }
}
