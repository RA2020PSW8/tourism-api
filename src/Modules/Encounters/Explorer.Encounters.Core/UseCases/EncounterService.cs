using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Enums;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService: CrudService<EncounterDto, Encounter>, IEncounterService
    {
        protected IEncounterRepository _encounterRepository;

        public EncounterService(IEncounterRepository encounterRepository, IMapper mapper): base(encounterRepository, mapper)
        {
            _encounterRepository = encounterRepository;
        }
        public Result<PagedResult<EncounterDto>> GetAllByStatus(string status)
        {
            if (Enum.TryParse<EncounterStatus>(status, out var encounterStatus))
            {
                var encounters = _encounterRepository.GetAllByStatus(encounterStatus);
                return MapToDto(encounters);
            }
            else
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid status value");
            }
        }
    }
}
