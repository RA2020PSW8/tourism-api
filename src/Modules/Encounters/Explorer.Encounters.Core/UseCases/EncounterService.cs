using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Enums;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using FluentResults;
using System.Security.Claims;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService: CrudService<EncounterDto, Encounter>, IEncounterService
    {
        protected IEncounterRepository _encounterRepository;
        protected IInternalTouristPositionService _touristPositionService;

        public EncounterService(IEncounterRepository encounterRepository, IInternalTouristPositionService touristPositionService, IMapper mapper): base(encounterRepository, mapper)
        {
            _encounterRepository = encounterRepository;
            _touristPositionService = touristPositionService;
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
        public Result<PagedResult<EncounterDto>> GetNearbyHidden(int page, int pageSize, int userId)
        {
            TouristPositionDto touristPosition = _touristPositionService.GetByUser(userId).Value;
            var encounters = _encounterRepository.GetNearbyByType(page, pageSize, touristPosition.Longitude, touristPosition.Latitude, EncounterType.LOCATION);
            return MapToDto(encounters);
        }
    }
}
