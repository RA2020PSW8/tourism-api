using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourAuthoring
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Get(int id);
        Result<TourDto> Create(TourDto equipment);
        Result<TourDto> Update(TourDto equipment);
        Result Delete(int id);

        Result<PagedResult<TourDto>> GetByAuthor(int page, int pageSize, int id);

        public Result<PagedResult<TourDto>> GetPublishedPaged(int page, int pageSize);
    }
}
