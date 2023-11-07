using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        Result<PagedResult<NotificationDto>> GetByUser(int page, int pageSize, int userId);
        Result<NotificationDto> Create(NotificationDto notification);
        Result<NotificationDto> Update(NotificationDto notification);
        Result Delete(int id);
    }
}
