using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ApplicationRatingDatabaseRepository : CrudDatabaseRepository<ApplicationRating, StakeholdersContext>, IApplicationRatingRepository
    {
        protected readonly StakeholdersContext DbContext;
        private readonly DbSet<ApplicationRating> _dbSet;

        public ApplicationRatingDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
            _dbSet = DbContext.Set<ApplicationRating>();
        }

        public ApplicationRating GetByUser(long userId)
        {
            var applicationRating = _dbSet.AsNoTracking().FirstOrDefault(ar => ar.UserId == userId);
            if (applicationRating == null) throw new KeyNotFoundException("Not found: " + userId);
            return applicationRating;
            
        }
    }
}
