using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository: ICrudRepository<Notification>
    {
        public PagedResult<Notification> GetByUser(int page, int pageSize, int userId);
    }
}
