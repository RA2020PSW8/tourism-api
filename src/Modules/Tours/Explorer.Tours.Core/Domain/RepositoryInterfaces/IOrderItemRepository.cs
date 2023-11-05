using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IOrderItemRepository: ICrudRepository<OrderItem>
    {
        public PagedResult<OrderItem> GetByUser(int page, int pageSize, int userId);
        OrderItem GetByUser(int userId);
    }
}
