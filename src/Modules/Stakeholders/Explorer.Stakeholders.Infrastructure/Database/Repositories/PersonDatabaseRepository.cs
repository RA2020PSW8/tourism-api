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
        private readonly DbSet<Person> _dbSet;

        public PersonDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Person>();
        }
        public Person GetOne(long personId)
        {
            // Define a SQL query using a recursive common table expression (CTE)
            
            /*Person person = _dbContext.People
       .Where(p => p.Id == personId)
       .FirstOrDefault();

            if (person != null)
            {
                _dbContext.Entry(person).Collection(p => p.Followers).Load();
                _dbContext.Entry(person).Collection(p => p.Followings).Load();
            }

            Person person = _dbContext.People
                       .Where(b => b.Id == personId)
                       .Include("Followings")
                       .Include("Followers")
                       .FirstOrDefault();*/
            //.IgnoreAutoIncludes();
            //var person = _dbContext.People.Find(personId);

            // Load the blog related to a given post.
            //_dbContext.Entry(person).Collection(p => p.Followers).Load();

            // Load the blog related to a given post using a string.
            //_dbContext.Entry(person).Collection("Followings").Load();

            /*var person = _dbContext.People.Find(personId);

            // Load the posts related to a given blog.
            _dbContext.Entry(person).Collection(p => p.Followers).Load();
            _dbContext.Entry(person).Collection(p => p.Followings).Load();
            */
            return person;
        }
    }
}
