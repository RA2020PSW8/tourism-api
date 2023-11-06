using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
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

        [HttpGet("all/{userId:int}")]
        public ActionResult<PagedResult<PersonDto>> GetNonFollowedProfiles([FromQuery] int page, [FromQuery] int pageSize, long userId)
        {
            var result = _profileService.GetUserNonFollowedProfiles(page, pageSize, userId);
            return CreateResponse(result);
        }

        [HttpGet("followers/{userId:int}")]
        public ActionResult<PagedResult<PersonDto>> GetFollowers(long userId)
        {
            var result = _profileService.GetFollowers(userId);
            return CreateResponse(result);
        }
        [HttpGet("following/{userId:int}")]
        public ActionResult<PagedResult<PersonDto>> GetFollowing(long userId)
        {
            var result = _profileService.GetFollowing(userId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PersonDto> Update(int id, [FromBody] PersonDto updatedPerson)
        {
            updatedPerson.Id = id;

            var result = _profileService.UpdateProfile(updatedPerson);
            return CreateResponse(result);
        }
        [HttpPut("follow/{followerId:long}")]
        public ActionResult<PagedResult<PersonDto>> Follow(long followerId, [FromBody] long followedId)
        {
            try
            {
                var result = _profileService.Follow(followerId, followedId);
                return CreateResponse(result);
            }
            catch (ArgumentException e)
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument).WithError(e.Message));
            }
        }
        [HttpPut("unfollow/{followerId:long}")]
        public ActionResult<PagedResult<PersonDto>> Unfollow(long followerId, [FromBody] long unfollowedId)
        {
            try
            {
                var result = _profileService.Unfollow(followerId, unfollowedId);
                return CreateResponse(result);
            }
            catch (ArgumentException e)
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument).WithError(e.Message));
            }
        }
    }
}
