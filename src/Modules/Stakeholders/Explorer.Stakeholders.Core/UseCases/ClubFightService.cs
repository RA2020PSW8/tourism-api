using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class ClubFightService : CrudService<ClubFightDto, ClubFight>, IClubFightService
{
    private readonly IClubFightRepository _fightRepository;
    
    public ClubFightService(IClubFightRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _fightRepository = repository;
    }

    public Result<ClubFightDto> CreateFromRequest(ClubChallengeRequestDto request)
    {
        ClubFight newClubFight = new ClubFight(null, DateTime.Now, request.ChallengerId, request.ChallengedId, true);
        var result = _fightRepository.Create(newClubFight);
        return MapToDto(result);
    }
}