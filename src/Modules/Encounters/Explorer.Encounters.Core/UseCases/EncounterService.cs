using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Enums;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService: CrudService<EncounterDto, Encounter>, IEncounterService
    {
        protected IEncounterRepository _encounterRepository;
        protected IInternalUserService _userService;
        protected IInternalProfileService _profileService;

        public EncounterService(IEncounterRepository encounterRepository, 
            IInternalUserService userService, IInternalProfileService profileService, IMapper mapper): base(encounterRepository, mapper)
        {
            _encounterRepository = encounterRepository;
            _userService = userService;
            _profileService = profileService;
        }

        public Result<PagedResult<EncounterDto>> GetApproved(int page, int pageSize)
        {
            var result = _encounterRepository.GetApproved(page, pageSize);
            return MapToDto(result);
        }

        public Result<PagedResult<EncounterDto>> GetApprovedByStatus(string status)
        {
            if (Enum.TryParse<EncounterStatus>(status, out var encounterStatus))
            {
                var encounters = _encounterRepository.GetApprovedByStatus(encounterStatus);
                return MapToDto(encounters);
            }
            else
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid status value");
            }
        }

        public Result<PagedResult<EncounterDto>> GetByUser(int page, int pageSize, long userId)
        {
            var result = _encounterRepository.GetByUser(page, pageSize, userId);
            return MapToDto(result);
        }

        public Result<PagedResult<EncounterDto>> GetTouristCreatedEncounters(int page, int pageSize)
        {
            var result = _encounterRepository.GetTouristCreatedEncounters(page, pageSize);
            return MapToDto(result);
        }

        public override Result<EncounterDto> Create(EncounterDto encounter)
        {
            try
            {
                UserDto user = _userService.Get(encounter.UserId).ValueOrDefault;
                if (user.Role == (int)UserRole.Tourist)
                {
                    PersonDto person = _profileService.Get(encounter.UserId).ValueOrDefault;
                    if (person.Level >= 10) 
                    {
                        encounter.ApprovalStatus = EncounterApprovalStatus.PENDING.ToString();
                        var result = _encounterRepository.Create(MapToDomain(encounter));
                        return MapToDto(result);
                    }
                    else
                    {
                        return Result.Fail("Encounter can not be created.");
                    }
                }
                else
                {
                    encounter.ApprovalStatus = EncounterApprovalStatus.SYSTEM_APPROVED.ToString();
                    var result = _encounterRepository.Create(MapToDomain(encounter));
                    return MapToDto(result);
                }               
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<EncounterDto> Approve(EncounterDto encounter)
        {
            try
            {
                encounter.ApprovalStatus = EncounterApprovalStatus.ADMIN_APPROVED.ToString();
                var result = _encounterRepository.Update(MapToDomain(encounter));
                return MapToDto(result);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<EncounterDto> Decline(EncounterDto encounter)
        {
            try
            {
                encounter.ApprovalStatus = EncounterApprovalStatus.DECLINED.ToString();
                var result = _encounterRepository.Update(MapToDomain(encounter));
                return MapToDto(result);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

    }
}
