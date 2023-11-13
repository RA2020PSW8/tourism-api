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
    public class OrderItemRepository: CrudDatabaseRepository<OrderItem, ToursContext>, IOrderItemRepository
    {
        protected readonly ToursContext _dbContext;
        private readonly DbSet<OrderItem> _dbSet;

        public OrderItemRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<OrderItem>();
        }
        public PagedResult<OrderItem> GetByUser(int page, int pageSize, int userId)
        {
            var task = _dbSet.Where(k => k.UserId == userId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public void RemoveRange(List<int> orderIds)
        {
            _dbSet.RemoveRange(_dbSet.Where(r => orderIds.Contains(Convert.ToInt32(r.Id))));
        }
    }
}
