using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class ClubFightService : CrudService<ClubFightDto, ClubFight>, IClubFightService, IInternalClubFightService
{
    private readonly IClubFightRepository _fightRepository;
    
    public ClubFightService(IClubFightRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _fightRepository = repository;
    }

    public Result<ClubFightDto> GetWithClubs(int fightId)
    {
        var result = _fightRepository.GetWithClubs(fightId);

        return MapToDto(result);
    }

    public Result<ClubFightDto> CreateFromRequest(ClubChallengeRequestDto request)
    {
        ClubFight existingFight = _fightRepository.GetCurrentFightForOneOfTwoClubs(request.ChallengerId, request.ChallengedId);
        if (existingFight != null)
        {
            return null;
        }

        ClubFight newClubFight = new ClubFight(null, DateTime.UtcNow, request.ChallengerId, request.ChallengedId, true);
        var result = _fightRepository.Create(newClubFight);
        return MapToDto(result);
    }
}