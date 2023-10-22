using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Tour;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Tour
{
    public class TourService : CrudService<TourDto, Domain.Tour>, ITourService
    {
        public TourService(ICrudRepository<Domain.Tour> repository, IMapper mapper) : base(repository, mapper) {}

        public Result<PagedResult<TourDto>> GetForAuthor(int page, int pageSize, int id)
        {
            var result = GetPaged(page, pageSize);
            result.Value.Results.Where(r => r.UserId == id).ToList();
            return result;
        }
    }
}
