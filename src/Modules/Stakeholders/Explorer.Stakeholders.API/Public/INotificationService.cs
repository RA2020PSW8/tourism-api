using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        Result<PagedResult<NotificationDto>> GetByUser(int page, int pageSize, int userId);
        Result<NotificationDto> Update(NotificationDto notification);
        Result Delete(int id);
    }
}
