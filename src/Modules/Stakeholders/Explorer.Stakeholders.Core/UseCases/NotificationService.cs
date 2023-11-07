using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class NotificationService : CrudService<NotificationDto, Notification>, INotificationService
    {
        protected readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _notificationRepository = repository;
        }
       
         public Result<PagedResult<NotificationDto>> GetByUser(int page, int pageSize, int userId)
        {
            var result = _notificationRepository.GetByUser(page, pageSize, userId);
            return MapToDto(result);
        }
    }
}
