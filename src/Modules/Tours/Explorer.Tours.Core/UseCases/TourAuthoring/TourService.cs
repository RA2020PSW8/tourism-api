using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class TourService : CrudService<TourDto, Domain.Tour>, ITourService, IInternalTourService
    {
        protected readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository, IMapper mapper) : base(tourRepository, mapper)
        {
            _tourRepository = tourRepository;
        }

        public Result<PagedResult<TourDto>> GetByAuthor(int page, int pageSize, int id)
        {
            var result = _tourRepository.GetByAuthorPaged(page, pageSize, id);
            return MapToDto(result);
        }

        public Result<PagedResult<TourDto>> GetPublishedPaged(int page, int pageSize)
        {
            var result = _tourRepository.GetPublishedPaged(page, pageSize);
            return MapToDto(result);
        }
        public TourDto GetById(int id)
        {
            var tour = _tourRepository.Get(id);
            return MapToDto(tour);
        }

        public Result<PagedResult<TourDto>> GetArchivedAndPublishedPaged(int page, int pageSize)
        {
            var result = _tourRepository.GetArchivedAndPublishedPaged(page, pageSize);
            return MapToDto(result);
        }
    }
}
