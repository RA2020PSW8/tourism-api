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
    public class PublicEntityRequestRepository : CrudDatabaseRepository<PublicEntityRequest, ToursContext>, IPublicEntityRequestRepository
    {
        private readonly DbSet<PublicEntityRequest> _dbSet;

        public PublicEntityRequestRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<PublicEntityRequest>();
        }
    }
}
