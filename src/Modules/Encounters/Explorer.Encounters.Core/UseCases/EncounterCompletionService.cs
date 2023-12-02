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

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterCompletionService : CrudService<EncounterCompletionDto, EncounterCompletion>, IEncounterCompletionService
    {
        protected IEncounterCompletionRepository _encounterCompletionRepository;
        protected IEncounterRepository _encounterRepository;
        protected IInternalTouristPositionService _touristPositionService;

        public EncounterCompletionService(IEncounterCompletionRepository encoutnerCompletionRepository, IInternalTouristPositionService touristPositionService,
            IEncounterRepository encounterRepository, IMapper mapper) : base(encoutnerCompletionRepository, mapper)
        {
            _encounterCompletionRepository = encoutnerCompletionRepository;
            _touristPositionService = touristPositionService;
            _encounterRepository = encounterRepository;
        }

        public Result<PagedResult<EncounterCompletionDto>> GetPagedByUser(int page, int pageSize, int userId)
        {
            var result = _encounterCompletionRepository.GetPagedByUser(page, page, userId);
            return MapToDto(result);
        }

        public EncounterCompletionDto GetByUserAndEncounter(int userId, int encounterId)
        {
            var result = _encounterCompletionRepository.GetByUserAndEncounter(userId, encounterId);
            return MapToDto(result);
        }

        public void UpdateSocialEncounters()
        {
            List<Encounter> socialEncounters = _encounterRepository.GetAllByStatusAndType(EncounterStatus.ACTIVE, EncounterType.SOCIAL).ToList();
            List<TouristPositionDto> touristPositions = _touristPositionService.GetPaged(0, 0).ValueOrDefault.Results;
            List<long> nearbyUserIds = new List<long>();

            foreach (var encounter in socialEncounters)
            {
                nearbyUserIds = touristPositions
                        .Where(position => IsTouristInRangeAndUpdated(position, encounter))
                        .Select(position => position.UserId)
                        .Distinct()
                        .ToList();

                foreach (long userId in nearbyUserIds)
                {
                    if (!_encounterCompletionRepository.HasUserStartedEncounter(userId, encounter.Id))
                    {
                        EncounterCompletion encounterCompletion = new EncounterCompletion(userId, encounter.Id, encounter.Xp, EncounterCompletionStatus.STARTED);
                        _encounterCompletionRepository.Create(encounterCompletion);
                    }

                    if (nearbyUserIds.Count >= encounter.PeopleCount)
                    {
                        if (!_encounterCompletionRepository.HasUserCompletedEncounter(userId, encounter.Id))
                        {
                            EncounterCompletion encounterCompletion = _encounterCompletionRepository.GetByUserAndEncounter(userId, encounter.Id);
                            encounterCompletion.UpdateStatus(EncounterCompletionStatus.COMPLETED);
                            _encounterCompletionRepository.Update(encounterCompletion);

                        }
                    }
                }
            }
        }
        public Result StartEncounter(long userId, EncounterDto encounter)
        {
            Result<TouristPositionDto> position = _touristPositionService.GetByUser(userId);
            EncounterCompletion encounterCompletion = new EncounterCompletion(userId, encounter.Id, encounter.Xp, EncounterCompletionStatus.STARTED);
            if (!_encounterCompletionRepository.HasUserStartedEncounter(userId, encounter.Id) && IsTouristInRange(position.Value, encounter.Longitude, encounter.Latitude, encounter.Range))
            {
                try
                {
                    _encounterCompletionRepository.Create(encounterCompletion);
                    return Result.Ok();
                }
                catch (KeyNotFoundException e)
                {
                    return Result.Fail(FailureCode.Conflict).WithError(e.Message);
                }
            }
            return Result.Fail(FailureCode.Conflict).WithError("This encounter can't be started");

        }
        public Result FinishEncounter(long userId, EncounterDto encounter)
        {
            var encounterCompletion = _encounterCompletionRepository.GetByUserAndEncounter(userId, encounter.Id);
            if (encounterCompletion == null) return Result.Fail(FailureCode.NotFound).WithError("You didn't started encounter yet");

            encounterCompletion.UpdateStatus(EncounterCompletionStatus.COMPLETED);
            _encounterCompletionRepository.Update(encounterCompletion);

            return Result.Ok();

        }
        private bool IsTouristInRangeAndUpdated(TouristPositionDto position, Encounter encounter)
        {
            bool isInRange = IsTouristInRange(position, encounter.Longitude, encounter.Latitude, encounter.Range);
            bool updatedRecently = position.UpdatedAt > DateTime.UtcNow.AddMinutes(-10);

            return isInRange && updatedRecently;
        }

        private bool IsTouristInRange(TouristPositionDto position, double longitude, double latitude, double range)
        {
            double touristDistance = DistanceCalculator.CalculateDistance(position.Latitude, position.Longitude, latitude, longitude);
            bool isInRange = touristDistance < range;
            return isInRange;
        }
    }
}
