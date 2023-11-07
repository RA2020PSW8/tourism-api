using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class NotificationRepository : CrudDatabaseRepository<Notification, StakeholdersContext>, INotificationRepository
    {
        private readonly DbSet<Notification> _dbSet;

        public NotificationRepository(StakeholdersContext dbContext) : base (dbContext) 
        {
            _dbSet = dbContext.Set<Notification>();
        }

        public PagedResult<Notification> GetByUser(int page, int pageSize, int userId) 
        {
            var task = _dbSet.Where(n => n.UserId == userId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

    }
}
