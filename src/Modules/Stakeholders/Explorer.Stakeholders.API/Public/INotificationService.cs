using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Enums;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        Result<PagedResult<NotificationDto>> GetByUser(int page, int pageSize, int userId);
        void Generate(int userId, NotificationType type, String actionURL, DateTime date, String additionalMessage);
        Result MarkAsRead(int id);
        Result Delete(int id);
    }
}
