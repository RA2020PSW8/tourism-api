using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "personPolicy")]
    [Route("api/profile")]
    public class ProfileController : BaseApiController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{userId:int}")]
        public ActionResult<AccountRegistrationDto> GetStakeholderProfile(long userId)
        {
            var result = _profileService.GetProfile(userId);
            return CreateResponse(result);
        }

        [HttpGet("all")]
        public ActionResult<PagedResult<PersonDto>> GetNonFollowedProfiles([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _profileService.GetUserNonFollowedProfiles(page, pageSize, ClaimsPrincipalExtensions.PersonId(User));
            return CreateResponse(result);
        }

        [HttpGet("followers")]
        public ActionResult<PagedResult<PersonDto>> GetFollowers()
        {
            var result = _profileService.GetFollowers(ClaimsPrincipalExtensions.PersonId(User));
            return CreateResponse(result);
        }
        [HttpGet("following")]
        public ActionResult<PagedResult<PersonDto>> GetFollowing()
        {
            var result = _profileService.GetFollowing(ClaimsPrincipalExtensions.PersonId(User));
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PersonDto> Update(int id, [FromBody] PersonDto updatedPerson)
        {
            updatedPerson.Id = id;

            var result = _profileService.UpdateProfile(updatedPerson);
            return CreateResponse(result);
        }
        [HttpPut("follow")]
        public ActionResult<PagedResult<PersonDto>> Follow([FromBody] long followedId)
        {
            try
            {
                var result = _profileService.Follow(ClaimsPrincipalExtensions.PersonId(User), followedId);
                return CreateResponse(result);
            }
            catch (ArgumentException e)
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument).WithError(e.Message));
            }
        }
        [HttpPut("unfollow")]
        public ActionResult<PagedResult<PersonDto>> Unfollow([FromBody] long unfollowedId)
        {
            try
            {
                var result = _profileService.Unfollow(ClaimsPrincipalExtensions.PersonId(User), unfollowedId);
                return CreateResponse(result);
            }
            catch (ArgumentException e)
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument).WithError(e.Message));
            }
        }
    }
}
