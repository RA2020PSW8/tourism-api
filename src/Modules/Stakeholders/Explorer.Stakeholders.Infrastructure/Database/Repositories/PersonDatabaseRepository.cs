using Explorer.BuildingBlocks.Core.UseCases;
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
    public class PersonDatabaseRepository : CrudDatabaseRepository<Person, StakeholdersContext>, IPersonRepository
    {

        private readonly StakeholdersContext _dbContext;

        public PersonDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Person>();
        }
        public Person GetFullProfile(long personId)
        { 
            Person person = _dbContext.People.Where(b => b.Id == personId)
                                             .Include("Following")
                                             .Include("Followers")
                                             .FirstOrDefault();
                                             //.IgnoreAutoIncludes();
            return person;
        }
    }
}
