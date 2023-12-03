using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Enums;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
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
        protected IInternalProfileService _profileService;

        public EncounterCompletionService(IEncounterCompletionRepository encoutnerCompletionRepository, IInternalTouristPositionService touristPositionService,
            IEncounterRepository encounterRepository, IInternalProfileService profileService, IMapper mapper) : base(encoutnerCompletionRepository, mapper)
        {
            _encounterCompletionRepository = encoutnerCompletionRepository;
            _touristPositionService = touristPositionService;
            _encounterRepository = encounterRepository;
            _profileService = profileService;
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
            List<Encounter> socialEncounters = _encounterRepository.GetApprovedByStatusAndType(EncounterStatus.ACTIVE, EncounterType.SOCIAL).ToList();
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
                            _profileService.AddXP((int)userId, encounter.Xp);

                        }
                    }
                }
            }
        }

        private bool IsTouristInRangeAndUpdated(TouristPositionDto position, Encounter encounter)
        {
            double touristDistance = DistanceCalculator.CalculateDistance(position.Latitude, position.Longitude, encounter.Latitude, encounter.Longitude);
            bool isInRange = touristDistance < encounter.Range;
            bool updatedRecently = position.UpdatedAt > DateTime.UtcNow.AddMinutes(-10);

            return isInRange && updatedRecently;
        }
    }
}
