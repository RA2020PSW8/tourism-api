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
        protected readonly IKeypointRepository _keypointRepository;

        public TourLifecycleService(ITourProgressRepository tourProgressRepository, ITourRepository tourRepository, ITouristPositionRepository touristPositionRepository,IKeypointRepository keypointRepository, IMapper mapper) : base(mapper)
        {
            _tourRepository = tourRepository;
            _touristPositionRepository = touristPositionRepository;
            _tourProgressRepository = tourProgressRepository;
            _keypointRepository = keypointRepository;
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

        public Result<TourProgressDto> UpdateActiveTour(long userId)
        {
            try
            {
                var tourProgress = _tourProgressRepository.GetActiveByUser(userId);
                try
                {
                    var currentKeypoint = _keypointRepository.GetByTourAndPosition(tourProgress.TourId, tourProgress.CurrentKeyPoint).FirstOrDefault();

                    var touristPosition = tourProgress.TouristPosition;


                    double dist = HaversineDistance(touristPosition.Latitude, touristPosition.Longitude, currentKeypoint.Latitude, currentKeypoint.Longitude);

                    if (dist <= 100)
                    {

                        List<int?> result = _keypointRepository.GetNextPositions(tourProgress.TourId, currentKeypoint.Position).ToList();
                        if (result.Count() == 0)
                        {
                            tourProgress.Complete();

                        }   
                        else
                        {

                            tourProgress.MoveToNextKeypoint(result[0] ?? 0);

                        }
                        touristPosition.UpdateTime();


                    }

                    tourProgress.UpdateActivityTime();
                    _tourProgressRepository.Update(tourProgress);
                    _touristPositionRepository.Update(touristPosition);
                    return MapToDto(tourProgress);
                }
                catch(KeyNotFoundException e)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Current keypoint not found.");
                }

            }
            catch(KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError("You cannot update tour that is not in progress."); 
            }

        }

        public static double HaversineDistance(double lat1, double lon1, double lat2, double lon2) {
            double R = 6371000; // Earth's radius [m] 

            double lat1Rad = ToRadians(lat1); 
            double lon1Rad = ToRadians(lon1);   
            double lat2Rad = ToRadians(lat2);
            double lon2Rad = ToRadians(lon2);

            double diffLon = lon2Rad - lon1Rad;
            double diffLat = lat2Rad - lat1Rad; 

            double a = Math.Pow(Math.Sin(diffLat/2),2) + Math.Cos(lat1Rad)*Math.Cos(lat2Rad)*Math.Pow(Math.Sin(diffLon/2),2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // distance [m] 

        }
        public static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0; 
        }
    }
}
