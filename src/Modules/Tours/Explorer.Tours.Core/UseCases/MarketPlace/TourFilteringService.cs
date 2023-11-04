using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.MarketPlace
{
    public class TourFilteringService :CrudService<TourDto, Tour>, ITourFilteringService
    {
        protected readonly ITourRepository _tourRepository;

        public TourFilteringService(ITourRepository tourRepository, IMapper mapper) : base(tourRepository, mapper) 
        {
            _tourRepository = tourRepository;
        }


        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadius = 6371; 

            
            double lat1Rad = DegreesToRadians(lat1);
            double lon1Rad = DegreesToRadians(lon1);
            double lat2Rad = DegreesToRadians(lat2);
            double lon2Rad = DegreesToRadians(lon2);

            
            double dLat = lat2Rad - lat1Rad;
            double dLon = lon2Rad - lon1Rad;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            
            double distance = earthRadius * c;

            return distance;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }
        
        public Result<PagedResult<TourDto>> GetFilteredTours(int page, int pageSize, TourFilterCriteriaDto filter)
        {
            try
            {
                var nearbyTours = _tourRepository.GetPublishedPaged(page, pageSize).Results
                .Where(tour =>
                    tour.Keypoints.Any(keyPoint =>
                        CalculateDistance(filter.CurrentLatitude, filter.CurrentLongitude, keyPoint.Latitude, keyPoint.Longitude) <= filter.FilterRadius))
                .Select(tour => MapToDto(tour)) 
                .ToList();

                var totalTours = nearbyTours.Count();
                var totalPages = (int)Math.Ceiling(totalTours / (double)pageSize);

                var pagedTours = nearbyTours.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var pagedResult = new PagedResult<TourDto>(pagedTours, totalPages);

                return Result.Ok(pagedResult);
            
            }
            catch (Exception e)
            {
                return Result.Fail<PagedResult<TourDto>>(e.Message);
            }
        }

      
    }
}
