using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public.Tourist;

public interface IClubService
{
    Result<PagedResult<ClubDto>> GetPaged(int page, int pageSize);
    Result<ClubDto> Create(ClubDto club);
    Result<ClubDto> Update(ClubDto club);

    Result Delete(int id);
    PagedResult<ClubDto> GetAllByUser(int page, int pageSize, int userId);
}