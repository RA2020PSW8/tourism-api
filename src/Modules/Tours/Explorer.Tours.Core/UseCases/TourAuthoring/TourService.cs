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

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class TourService : CrudService<TourDto, Domain.Tour>, ITourService
    {
        protected readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository, IMapper mapper) : base(tourRepository, mapper)
        {
            _tourRepository = tourRepository;
        }

        public Result<PagedResult<TourDto>> GetForAuthor(int page, int pageSize, int id)
        {
            var result = GetPaged(page, pageSize);
            result.Value.Results.Where(r => r.UserId == id).ToList();
            return result;
        }

        public Result<PagedResult<TourDto>> GetPublishedPaged(int page, int pageSize)
        {
            var result = _tourRepository.GetPublishedPaged(page, pageSize);
            return MapToDto(result);
        }
    }
}
