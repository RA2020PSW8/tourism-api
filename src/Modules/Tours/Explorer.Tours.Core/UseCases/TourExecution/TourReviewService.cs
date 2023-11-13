using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecution
{
    public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
    {
        protected readonly ITourReviewRepository _tourReviewRepository;
        public TourReviewService(ICrudRepository<TourReview> crudRepository, IMapper mapper, ITourReviewRepository tourReviewRepository) : base(crudRepository, mapper)
        {
            _tourReviewRepository = tourReviewRepository;
        }

        public Result<PagedResult<TourReviewDto>> GetByTourId(long tourId, int page, int pageSize)
        {
            var result = _tourReviewRepository.GetByTourId(tourId, page, pageSize);
            return MapToDto(result);
        }

        public Result<double> CalculateAverageRate(List<TourReviewDto> tourReviews)
        {
            if (tourReviews == null || !tourReviews.Any())
            {
                return Result.Fail<double>("There are no tour reviews!");
            }

            double averageRate = tourReviews.Average(r => r.Rating);

            return Result.Ok(averageRate);
        }
    }
}
