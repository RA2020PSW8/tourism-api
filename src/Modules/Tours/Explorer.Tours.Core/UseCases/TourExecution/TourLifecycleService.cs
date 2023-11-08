using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecution
{
    public class TourLifecycleService : BaseService<TourProgressDto, TourProgress>, ITourLifecycleService
    {
        protected readonly ITourRepository _tourRepository; 
        protected readonly ITouristPositionRepository _touristPositionRepository;
        protected readonly ITourProgressRepository _tourProgressRepository;

        public TourLifecycleService(ITourProgressRepository tourProgressRepository, ITourRepository tourRepository, ITouristPositionRepository touristPositionRepository, IMapper mapper) : base(mapper)
        {
            _tourRepository = tourRepository;
            _touristPositionRepository = touristPositionRepository;
            _tourProgressRepository = tourProgressRepository;
        }

        public Result<TourProgressDto> GetActiveByUser(long userId)
        {
            try
            {
                var tourProgress = _tourProgressRepository.GetActiveByUser(userId);
                return MapToDto(tourProgress);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError("You don't have any started tours.");
            }
        }

        public Result<TourProgressDto> StartTour(long tourId, long userId)
        {
            try
            {
                var tour = _tourRepository.Get(tourId);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError("Tour you want to start doesn't exist.");
            }

            // try to remove nested try-catch
            try
            {
                var touristPosition = _touristPositionRepository.GetByUser(userId);

                try
                {
                    var existingTourProgress = _tourProgressRepository.GetActiveByUser(userId);
                    return Result.Fail(FailureCode.NotFound).WithError("You already have tour that's in progress.");
                }
                catch (KeyNotFoundException e)
                {
                    TourProgress tourProgress = new TourProgress(touristPosition.Id, tourId);
                    _tourProgressRepository.Create(tourProgress);

                    return MapToDto(tourProgress);
                }
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError("You can't start tour without setting your current location first.");
            }
        }

        public Result<TourProgressDto> AbandonActiveTour(long userId)
        {
            try
            {
                var tourProgress = _tourProgressRepository.GetActiveByUser(userId);

                tourProgress.Abandon();
                _tourProgressRepository.Update(tourProgress);
                
                return MapToDto(tourProgress);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError("You cannot abandon tour that is not in progress.");
            }
        }
    }
}
