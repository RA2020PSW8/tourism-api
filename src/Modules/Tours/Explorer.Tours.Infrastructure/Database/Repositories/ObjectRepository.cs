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
    public class ObjectRepository : CrudDatabaseRepository<Core.Domain.Object, ToursContext>, IObjectRepository
    {
        private readonly DbSet<Core.Domain.Object> _dbSet;

        public ObjectRepository(ToursContext dbContext) : base(dbContext) 
        {
            _dbSet = dbContext.Set<Core.Domain.Object>();
        }
    }
}
