using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourReviewRepository : CrudDatabaseRepository<TourReview, ToursContext>, ITourReviewRepository
    {
        private readonly DbSet<TourReview> _dbSet;

        public TourReviewRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TourReview>();
        }

        public PagedResult<TourReview> GetByTourId(long tourId, int page, int pageSize)
        {
            var task = _dbSet.Where(k => k.TourId == tourId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
