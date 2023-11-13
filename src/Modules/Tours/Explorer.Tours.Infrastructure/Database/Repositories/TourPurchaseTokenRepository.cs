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
    public class TourPurchaseTokenRepository : CrudDatabaseRepository<TourPurchaseToken, ToursContext>, ITourPurchaseTokenRepository
    {
        private readonly DbSet<TourPurchaseToken> _dbSet;

        public TourPurchaseTokenRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TourPurchaseToken>();
        }

        public void AddRange(List<TourPurchaseToken> tokens)
        {
            _dbSet.AddRange(tokens);
            DbContext.SaveChanges();
        }

        public TourPurchaseToken GetByTourAndTourist (int tourId, int touristId)
        {
            return DbContext.TourPurchaseTokens.FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);
        }

    }
}
