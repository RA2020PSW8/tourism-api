﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.TourExecution;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases.TourExecution
{
    public class TourIssueCommentService : CrudService<TourIssueCommentDto, TourIssueComment>, ITourIssueCommentService
    {
        private readonly INotificationService _notificationService;
        private readonly ITourIssueService _tourIssueService;
        private readonly IUserService _userService;
        private readonly IInternalTourService _tourService;

        public TourIssueCommentService(ICrudRepository<TourIssueComment> crudRepository, IMapper mapper,
            INotificationService notificationService, ITourIssueService tourIssueService, 
            IUserService userService, IInternalTourService tourService) : base(crudRepository, mapper)
        {
            _notificationService = notificationService;
            _tourIssueService = tourIssueService;
            _userService = userService;
            _tourService = tourService;
        }
        
        override public Result<TourIssueCommentDto> Create(TourIssueCommentDto entity)
        {
            TourIssueDto tourIssue = _tourIssueService.Get(entity.TourIssueId).Value;
            TourDto tour;

            try
            {
                tour = _tourService.Get(tourIssue.TourId).Value;
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            int notificationUserId = _userService.Get(entity.UserId).Value.Role == 1 ? tourIssue.UserId : tour.UserId;

            NotificationDto notification = new NotificationDto();
            notification.UserId = notificationUserId;
            notification.Content = "A new comment has been added to the tour issue for the tour named '" + tour.Name + "'.";
            notification.ActionURL = "url"; //figure out this later
            notification.CreationDateTime = DateTime.UtcNow;
            notification.IsRead = false;

            _notificationService.Create(notification);

            var result = CrudRepository.Create(MapToDomain(entity));
            return MapToDto(result);
        }
        
    }
}
