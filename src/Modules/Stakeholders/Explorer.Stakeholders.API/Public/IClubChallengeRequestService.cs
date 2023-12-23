using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubChallengeRequestService
{
    Result<ClubChallengeRequestDto> Create(ClubChallengeRequestDto request);
    Result<ClubChallengeRequestDto> Update(ClubChallengeRequestDto request);
}