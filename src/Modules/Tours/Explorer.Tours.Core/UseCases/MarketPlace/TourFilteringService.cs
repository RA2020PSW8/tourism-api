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
    public class TourFilteringService :BaseService<TourDto, Tour>, ITourFilteringService
    {
        protected readonly ITourRepository _tourRepository;

        public TourFilteringService(ITourRepository tourRepository, IMapper mapper) : base( mapper) 
        {
            _tourRepository = tourRepository;
        }
      
        public Result<PagedResult<TourDto>> GetFilteredTours(int page, int pageSize, TourFilterCriteriaDto filter)
        {
            try
            {
                var nearbyTours = _tourRepository.GetPublishedPaged(page, pageSize).Results
                .Where(tour =>
                    tour.Keypoints.Any(keyPoint =>
                        DistanceCalculator.CalculateDistance(filter.CurrentLatitude, filter.CurrentLongitude, keyPoint.Latitude, keyPoint.Longitude) <= filter.FilterRadius))
                .Select(tour => MapToDto(tour))
                .ToList();

                var totalTours = nearbyTours.Count();
                var totalPages = (int)Math.Ceiling(totalTours / (double)pageSize);
                var pagedTours = nearbyTours;
                if (page != 0 && pageSize != 0)
                {
                    pagedTours = nearbyTours.Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }

                var pagedResult = new PagedResult<TourDto>(pagedTours, totalTours);

                return Result.Ok(pagedResult);
            }
            catch (Exception e)
            {
                return Result.Fail<PagedResult<TourDto>>(e.Message);
            }
        }
    }
}
