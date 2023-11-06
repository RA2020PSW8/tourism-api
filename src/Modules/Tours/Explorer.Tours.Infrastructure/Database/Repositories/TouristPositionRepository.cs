using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TouristPositionRepository : CrudDatabaseRepository<TouristPosition, ToursContext>, ITouristPositionRepository
    {
        private readonly DbSet<TouristPosition> _dbSet;

        public TouristPositionRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TouristPosition>();
        }

        public TouristPosition GetByUser(long userId)
        {
            var touristPosition = _dbSet.AsNoTracking().FirstOrDefault(tp => tp.UserId == userId);
            if (touristPosition == null) throw new KeyNotFoundException("Not found: " + userId);
            return touristPosition;
        }
    }
}
