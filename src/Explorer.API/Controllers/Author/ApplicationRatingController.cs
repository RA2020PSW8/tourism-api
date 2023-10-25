using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route ("api/author/appRating")]
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

        [HttpGet]
        public ActionResult<ApplicationRatingDto> GetByUserId()
        {
            int userId = ClaimsPrincipalExtensions.PersonId(User);
            var result = _applicationRatingService.GetByUserId(0, 0, userId);
            return CreateResponse(result);
        }

    }
}
