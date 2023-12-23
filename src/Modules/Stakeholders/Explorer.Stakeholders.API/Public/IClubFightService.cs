using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubFightService
{
    Result<ClubFightDto> CreateFromRequest(ClubChallengeRequestDto request);
    Result<ClubFightDto> Update(ClubFightDto clubFightDto);   
}