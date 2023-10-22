using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/appRating")]
    public class ApplicationRatingController : BaseApiController
    {
        private readonly IApplicationRatingService _applicationRatingService;

        public ApplicationRatingController(IApplicationRatingService applicationRatingService)
        {
            _applicationRatingService = applicationRatingService;
        }

        [HttpPost]
        public ActionResult<ApplicationRatingDto> Create([FromBody] ApplicationRatingDto applicationRatingDto)
        {
            var result = _applicationRatingService.Create(applicationRatingDto);
            return CreateResponse(result);
        }

        [HttpPut("{username}")]
        public ActionResult<ApplicationRatingDto> Update([FromBody] ApplicationRatingDto applicationRatingDto, [FromRoute] string username)
        {
            var result = _applicationRatingService.Update(applicationRatingDto);
            return CreateResponse(result);
        }
    }
}
