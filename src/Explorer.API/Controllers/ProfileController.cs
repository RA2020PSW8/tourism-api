using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
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
        public ActionResult<AccountRegistrationDto> getStakeholderProfile(long userId)
        {
            var result = _profileService.GetProfile(userId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PersonDto> Update(int id, [FromBody] PersonDto updatedPerson)
        {
            updatedPerson.Id = id;

            var result = _profileService.UpdateProfile(updatedPerson);
            return CreateResponse(result);
        }
        [HttpPut("{follower:long}/{following:long}")]
        public ActionResult<PersonDto> FollowProfile(long follower, long following)
        {
            //if (  _profileService.IsProfileAlreadyFollowed(follower, following)) return CreateResponse(Result.Fail(FailureCode.Conflict));

            try
            {

                var result = _profileService.FollowProfile(follower, following);
                return CreateResponse(result);
            }
            catch (ArgumentException e)
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument).WithError(e.Message));
            }
        }
    }
}
