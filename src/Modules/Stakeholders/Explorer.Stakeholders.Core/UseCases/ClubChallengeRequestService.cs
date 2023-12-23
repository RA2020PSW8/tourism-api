using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class ClubChallengeRequestService : BaseService<ClubChallengeRequestDto, ClubChallengeRequest>, IClubChallengeRequestService
{
    private readonly IClubChallengeRequestRepository _challengeRequestRepository;
    private readonly IClubService _clubService;

    public ClubChallengeRequestService(IClubChallengeRequestRepository repository, IClubService clubService, IMapper mapper): base(mapper)
    {
        _challengeRequestRepository = repository;
        _clubService = clubService;
    }

    public Result<ClubChallengeRequestDto> Create(ClubChallengeRequestDto request)
    {
        ClubDto Challenger = _clubService.Get((int)request.ChallengerId).Value;
        ClubDto Challanged = _clubService.Get((int)request.ChallengedId).Value;
        
        request.Status = "PENDING";
        request.Challenger = Challenger;
        request.Challenged = Challanged;
        
        var result = _challengeRequestRepository.Create(MapToDomain(request));
        return MapToDto(result);
    }

    public Result<ClubChallengeRequestDto> Update(ClubChallengeRequestDto request)
    {
        var result = _challengeRequestRepository.Update(MapToDomain(request));
        return MapToDto(result);
    }
}