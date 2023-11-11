using Explorer.BuildingBlocks.Core.Domain;
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
    public class TourPreferenceDatabaseRepository : CrudDatabaseRepository<TourPreference, ToursContext>, ITourPreferenceRepository
    {
        private readonly DbSet<TourPreference> _dbSet;

        public TourPreferenceDatabaseRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbSet = DbContext.Set<TourPreference>();
        }

        public TourPreference GetByUser(long userId)
        {
            var tourPreference = _dbSet.AsNoTracking().FirstOrDefault(tp => tp.UserId == userId);
            if (tourPreference == null) throw new KeyNotFoundException("Not found: " + userId);
            return tourPreference;
        }
    }
}
