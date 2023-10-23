using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.Tourist
{
    public class ClubJoinRequestService : CrudService<ClubJoinRequestDto, ClubJoinRequest>, IClubJoinRequestService
    {
        protected readonly IClubJoinRequestRepository _requestRepository;
        public ClubJoinRequestService(IClubJoinRequestRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _requestRepository = repository;
        }

        public Result<PagedResult<ClubJoinRequestDto>> GetAllByUser(int userId)
        {
            try
            {
                var requests = _requestRepository.GetAllByUser(userId);
                return MapToDto(requests);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        public Result<PagedResult<ClubJoinRequestDto>> GetAllByClub(int clubId)
        {
            try
            {
                var requests = _requestRepository.GetAllByClub(clubId);
                return MapToDto(requests);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        override public Result<ClubJoinRequestDto> Create(ClubJoinRequestDto entity)
        {
            if (_requestRepository.Exists(entity.ClubId, entity.UserId)) return Result.Fail(FailureCode.Conflict).WithError("Request for this user already exists");
            try
            {
                var result = CrudRepository.Create(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {  
                return Result.Fail(FailureCode.InvalidArgument).WithError($"Error while creating request: {e.Message}");
            }
        }
    }
}
