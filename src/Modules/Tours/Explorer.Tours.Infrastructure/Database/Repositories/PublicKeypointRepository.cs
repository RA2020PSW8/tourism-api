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
    public class PublicKeypointRepository : CrudDatabaseRepository<PublicKeypoint, ToursContext>, IPublicKeypointRepository
    {
        private readonly DbSet<PublicKeypoint> _dbSet;

        public PublicKeypointRepository( ToursContext dbContext) : base(dbContext) 
        {
            _dbSet = dbContext.Set<PublicKeypoint>();
        }
    }
}
