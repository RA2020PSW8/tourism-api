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
    public interface IPublicKeypointService
    {
        Result<PagedResult<PublicKeypointDto>> GetPaged(int page, int pageSize);
        Result<PublicKeypointDto> Create(PublicKeypointDto publicKeypoint);
        Result<PublicKeypointDto> Update(PublicKeypointDto publicKeypoint);
        Result Delete(int id);
        Result<PagedResult<PublicKeypointDto>> GetPagedInRange(int page, int pageSize, FilterCriteriaDto filter);
    }
}
