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
    public class TourIssueDatabaseRepository : CrudDatabaseRepository<TourIssue, StakeholdersContext>, ITourIssueRepository
    {
        private readonly DbSet<TourIssue> _dbSet;
        public TourIssueDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TourIssue>();
        }
        public PagedResult<TourIssue> GetByUserPaged(int page, int pageSize, int userId)
        {
            var task = _dbSet.Where(t => t.UserId == userId).Include(t => t.Comments).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
