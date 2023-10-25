using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;

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
    }
}
