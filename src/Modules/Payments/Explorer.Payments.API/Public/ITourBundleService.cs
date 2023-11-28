using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ITourBundleService
    {
        Result<PagedResult<TourBundleDto>> GetPaged(int page, int pageSize);
        Result<TourBundleDto> Create(TourBundleDto bundle);
        Result<TourBundleDto> Update(TourBundleDto bundle);
       
        Result Delete(int id);
    }
}
