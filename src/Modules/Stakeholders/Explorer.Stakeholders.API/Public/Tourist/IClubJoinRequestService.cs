using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public.Tourist
{
    public interface IClubJoinRequestService
    {
        Result<PagedResult<ClubJoinRequestDto>> GetPaged(int page, int pageSize);
        Result<List<ClubJoinRequestDto>> GetAllByUserId(int id);
        Result<ClubJoinRequestDto> Create(ClubJoinRequestDto request);
        Result<ClubJoinRequestDto> Update(ClubJoinRequestDto request);
    }
}
